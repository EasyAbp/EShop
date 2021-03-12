using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.Permissions;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;
using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class BasketItemAppService : CrudAppService<BasketItem, BasketItemDto, Guid, GetBasketItemListDto, CreateBasketItemDto, UpdateBasketItemDto>,
        IBasketItemAppService
    {
        protected override string GetPolicyName { get; set; } = BasketsPermissions.BasketItem.Default;
        protected override string GetListPolicyName { get; set; } = BasketsPermissions.BasketItem.Default;
        protected override string CreatePolicyName { get; set; } = BasketsPermissions.BasketItem.Create;
        protected override string UpdatePolicyName { get; set; } = BasketsPermissions.BasketItem.Update;
        protected override string DeletePolicyName { get; set; } = BasketsPermissions.BasketItem.Delete;

        private readonly IBasketItemRepository _repository;
        private readonly IProductUpdateRepository _productUpdateRepository;
        private readonly IProductAppService _productAppService;
        private readonly IProductSkuDescriptionProvider _productSkuDescriptionProvider;

        public BasketItemAppService(
            IBasketItemRepository repository,
            IProductUpdateRepository productUpdateRepository,
            IProductAppService productAppService,
            IProductSkuDescriptionProvider productSkuDescriptionProvider) : base(repository)
        {
            _repository = repository;
            _productUpdateRepository = productUpdateRepository;
            _productAppService = productAppService;
            _productSkuDescriptionProvider = productSkuDescriptionProvider;
        }

        public override async Task<BasketItemDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var item = await GetEntityByIdAsync(id);
            
            if (item.UserId != CurrentUser.GetId() && !await IsCurrentUserManagerAsync())
            {
                throw new AbpAuthorizationException();
            }

            var productUpdate = await _productUpdateRepository.FindAsync(x => x.ProductSkuId == item.ProductSkuId);
            
            if (productUpdate != null)
            {
                var itemUpdateTime = item.LastModificationTime ?? item.CreationTime;
                var productUpdateTime = productUpdate.LastModificationTime ?? productUpdate.CreationTime;

                if (itemUpdateTime < productUpdateTime)
                {
                    var productDto = await _productAppService.GetAsync(item.ProductId);
                    
                    await UpdateProductDataAsync(item.Quantity, item, productDto);
                    
                    await _repository.UpdateAsync(item, true);
                }
            }

            return await MapToGetOutputDtoAsync(item);
        }

        public override async Task<PagedResultDto<BasketItemDto>> GetListAsync(GetBasketItemListDto input)
        {
            await CheckGetListPolicyAsync();
            
            if (input.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(BasketsPermissions.BasketItem.Manage);
            }

            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var items = await AsyncExecuter.ToListAsync(query);

            var productSkuIds = items.Select(item => item.ProductSkuId).ToList();

            var skuIdUpdateTimeDict =
                (await _productUpdateRepository.GetListByProductSkuIdsAsync(productSkuIds)).ToDictionary(
                    x => x.ProductSkuId, x => x.LastModificationTime ?? x.CreationTime);

            var productDtoDict = new Dictionary<Guid, ProductDto>();

            foreach (var item in items)
            {
                if (!skuIdUpdateTimeDict.ContainsKey(item.ProductSkuId))
                {
                    continue;
                }

                var itemUpdateTime = item.LastModificationTime ?? item.CreationTime;
                var productUpdateTime = skuIdUpdateTimeDict[item.ProductSkuId];

                if (itemUpdateTime >= productUpdateTime)
                {
                    continue;
                }

                if (!productDtoDict.ContainsKey(item.ProductId))
                {
                    // Todo: deleted product cause errors
                    productDtoDict[item.ProductId] = await _productAppService.GetAsync(item.ProductId);
                }

                await UpdateProductDataAsync(item.Quantity, item, productDtoDict[item.ProductId]);

                await _repository.UpdateAsync(item);
            }

            return new PagedResultDto<BasketItemDto>(
                totalCount,
                await MapToGetListOutputDtosAsync(items)
            );
        }
        
        protected virtual async Task UpdateProductDataAsync(int quantity, BasketItem item, ProductDto productDto)
        {
            item.SetIsInvalid(false);

            var productSkuDto = productDto.FindSkuById(item.ProductSkuId);

            if (productSkuDto == null)
            {
                item.SetIsInvalid(true);
                
                return;
            }

            if (productDto.InventoryStrategy != InventoryStrategy.NoNeed && quantity > productSkuDto.Inventory)
            {
                item.SetIsInvalid(true);
            }
            
            item.UpdateProductData(quantity, new ProductDataModel
            {
                MediaResources = productSkuDto.MediaResources ?? productDto.MediaResources,
                ProductUniqueName = productDto.UniqueName,
                ProductDisplayName = productDto.DisplayName,
                SkuName = productSkuDto.Name,
                SkuDescription = await _productSkuDescriptionProvider.GenerateAsync(productDto, productSkuDto),
                Currency = productSkuDto.Currency,
                UnitPrice = productSkuDto.DiscountedPrice,
                TotalPrice = productSkuDto.DiscountedPrice * item.Quantity,
                TotalDiscount = (productSkuDto.Price - productSkuDto.DiscountedPrice) * item.Quantity,
                Inventory = productSkuDto.Inventory
            });

            if (!productDto.IsPublished)
            {
                item.SetIsInvalid(true);
            }
        }

        protected override async Task<IQueryable<BasketItem>> CreateFilteredQueryAsync(GetBasketItemListDto input)
        {
            var userId = input.UserId ?? CurrentUser.GetId();
            
            return ReadOnlyRepository.Where(item => item.UserId == userId && item.BasketName == input.BasketName);
        }

        public override async Task<BasketItemDto> CreateAsync(CreateBasketItemDto input)
        {
            await CheckCreatePolicyAsync();

            var userId = input.UserId ?? CurrentUser.GetId();

            if (userId != CurrentUser.GetId() && !await IsCurrentUserManagerAsync())
            {
                throw new AbpAuthorizationException();
            }

            var productDto = await _productAppService.GetAsync(input.ProductId);

            var item = await _repository.FindAsync(x =>
                x.UserId == userId && x.BasketName == input.BasketName && x.ProductSkuId == input.ProductSkuId);

            if (item != null)
            {
                await UpdateProductDataAsync(input.Quantity + item.Quantity, item, productDto);
            
                await Repository.UpdateAsync(item, autoSave: true);

                return await MapToGetOutputDtoAsync(item);
            }
            
            var productSkuDto = productDto.FindSkuById(input.ProductSkuId);

            if (productSkuDto == null)
            {
                throw new ProductSkuNotFoundException(input.ProductId, input.ProductSkuId);
            }

            item = new BasketItem(GuidGenerator.Create(), CurrentTenant.Id, input.BasketName, CurrentUser.GetId(),
                productDto.StoreId, input.ProductId, input.ProductSkuId);

            await UpdateProductDataAsync(input.Quantity, item, productDto);
            
            await Repository.InsertAsync(item, autoSave: true);

            return await MapToGetOutputDtoAsync(item);
        }

        public override async Task<BasketItemDto> UpdateAsync(Guid id, UpdateBasketItemDto input)
        {
            await CheckUpdatePolicyAsync();

            var item = await GetEntityByIdAsync(id);
            
            if (item.UserId != CurrentUser.GetId() && !await IsCurrentUserManagerAsync())
            {
                throw new AbpAuthorizationException();
            }

            var productDto = await _productAppService.GetAsync(item.ProductId);

            await UpdateProductDataAsync(input.Quantity, item, productDto);
            
            await Repository.UpdateAsync(item, autoSave: true);

            return await MapToGetOutputDtoAsync(item);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await CheckDeletePolicyAsync();

            var item = await _repository.GetAsync(id);

            if (item.UserId != CurrentUser.GetId() && !await IsCurrentUserManagerAsync())
            {
                throw new AbpAuthorizationException();
            }
            
            await _repository.DeleteAsync(item, true);
        }

        public virtual async Task DeleteInBulkAsync(IEnumerable<Guid> ids)
        {
            await CheckDeletePolicyAsync();
            
            var isCurrentUserManager = await IsCurrentUserManagerAsync();
            
            foreach (var id in ids)
            {
                var item = await GetEntityByIdAsync(id);
                
                if (item.UserId != CurrentUser.GetId() && !isCurrentUserManager)
                {
                    throw new AbpAuthorizationException();
                }

                await _repository.DeleteAsync(item);
            }
        }

        protected virtual async Task<bool> IsCurrentUserManagerAsync()
        {
            return await AuthorizationService.IsGrantedAsync(BasketsPermissions.BasketItem.Manage);
        }
    }
}
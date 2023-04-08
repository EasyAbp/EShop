using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.Permissions;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class BasketItemAppService : CrudAppService<BasketItem, BasketItemDto, Guid, GetBasketItemListDto,
            CreateBasketItemDto, UpdateBasketItemDto>,
        IBasketItemAppService
    {
        protected override string GetPolicyName { get; set; } = BasketsPermissions.BasketItem.Default;
        protected override string GetListPolicyName { get; set; } = BasketsPermissions.BasketItem.Default;
        protected override string CreatePolicyName { get; set; } = BasketsPermissions.BasketItem.Default;
        protected override string UpdatePolicyName { get; set; } = BasketsPermissions.BasketItem.Default;
        protected override string DeletePolicyName { get; set; } = BasketsPermissions.BasketItem.Default;

        private readonly IBasketItemRepository _repository;
        private readonly IProductUpdateRepository _productUpdateRepository;
        private readonly IProductAppService _productAppService;

        public BasketItemAppService(
            IBasketItemRepository repository,
            IProductUpdateRepository productUpdateRepository,
            IProductAppService productAppService) : base(repository)
        {
            _repository = repository;
            _productUpdateRepository = productUpdateRepository;
            _productAppService = productAppService;
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
                items.Count,
                await MapToGetListOutputDtosAsync(items)
            );
        }

        protected virtual async Task UpdateProductDataAsync(int targetQuantity, IBasketItem item, ProductDto productDto)
        {
            item.SetIsInvalid(false);

            var updaters = LazyServiceProvider.LazyGetRequiredService<IEnumerable<IBasketItemProductInfoUpdater>>();

            if (CurrentUser.IsAuthenticated)
            {
                foreach (var updater in updaters)
                {
                    await updater.UpdateForIdentifiedAsync(targetQuantity, item, productDto);
                }
            }
            else
            {
                foreach (var updater in updaters)
                {
                    await updater.UpdateForAnonymousAsync(targetQuantity, item, productDto);
                }
            }
        }

        protected override async Task<IQueryable<BasketItem>> CreateFilteredQueryAsync(GetBasketItemListDto input)
        {
            var userId = input.UserId ?? CurrentUser.GetId();

            return (await ReadOnlyRepository.GetQueryableAsync())
                .Where(item => item.UserId == userId && item.BasketName == input.BasketName);
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

            var productSkuDto = (ProductSkuDto)productDto.FindSkuById(input.ProductSkuId);

            if (productSkuDto == null)
            {
                throw new ProductSkuNotFoundException(input.ProductId, input.ProductSkuId);
            }

            item = new BasketItem(GuidGenerator.Create(), CurrentTenant.Id, input.BasketName, CurrentUser.GetId(),
                productDto.StoreId, input.ProductId, input.ProductSkuId, productSkuDto.ProductDiscounts,
                productSkuDto.OrderDiscountPreviews);

            input.MapExtraPropertiesTo(item);

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

            input.MapExtraPropertiesTo(item);

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

        public virtual async Task BatchDeleteAsync(IEnumerable<Guid> ids)
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

        public virtual async Task<ListResultDto<ClientSideBasketItemModel>> GenerateClientSideDataAsync(
            GenerateClientSideDataInput input)
        {
            var itemList = new List<ClientSideBasketItemModel>();

            var products = new Dictionary<Guid, ProductDto>();

            foreach (var dto in input.Items)
            {
                if (!products.ContainsKey(dto.ProductId))
                {
                    products[dto.ProductId] = await _productAppService.GetAsync(dto.ProductId);
                }

                var productDto = products[dto.ProductId];

                var productSkuDto = (ProductSkuDto)productDto.FindSkuById(dto.ProductSkuId);

                if (productSkuDto == null)
                {
                    throw new ProductSkuNotFoundException(dto.ProductId, dto.ProductSkuId);
                }

                var id = dto.Id ?? GuidGenerator.Create();

                var item = new ClientSideBasketItemModel(id, dto.BasketName, productDto.StoreId, dto.ProductId,
                    dto.ProductSkuId, productSkuDto.ProductDiscounts, productSkuDto.OrderDiscountPreviews);

                await UpdateProductDataAsync(dto.Quantity, item, productDto);

                itemList.Add(item);
            }

            return new ListResultDto<ClientSideBasketItemModel>(itemList);
        }

        protected virtual async Task<bool> IsCurrentUserManagerAsync()
        {
            return await AuthorizationService.IsGrantedAsync(BasketsPermissions.BasketItem.Manage);
        }
    }
}
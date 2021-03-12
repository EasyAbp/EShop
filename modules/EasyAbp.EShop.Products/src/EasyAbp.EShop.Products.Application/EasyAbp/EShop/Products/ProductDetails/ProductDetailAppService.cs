using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public class ProductDetailAppService : CrudAppService<ProductDetail, ProductDetailDto, Guid, GetProductDetailListInput, CreateUpdateProductDetailDto, CreateUpdateProductDetailDto>,
        IProductDetailAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Products.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Products.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Products.Update;
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Default;

        private readonly IProductRepository _productRepository;
        private readonly IProductDetailRepository _repository;

        public ProductDetailAppService(
            IProductRepository productRepository,
            IProductDetailRepository repository) : base(repository)
        {
            _productRepository = productRepository;
            _repository = repository;
        }

        protected override async Task<IQueryable<ProductDetail>> CreateFilteredQueryAsync(GetProductDetailListInput input)
        {
            return _repository.WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value);
        }

        public override async Task<PagedResultDto<ProductDetailDto>> GetListAsync(GetProductDetailListInput input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId, GetListPolicyName,
                ProductsPermissions.Products.CrossStore);
            
            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var productDetails = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(productDetails);

            return new PagedResultDto<ProductDetailDto>(
                totalCount,
                entityDtos
            );
        }

        public override async Task<ProductDetailDto> CreateAsync(CreateUpdateProductDetailDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName,
                ProductsPermissions.Products.CrossStore);
            
            var productDetail = await MapToEntityAsync(input);

            TryToSetTenantId(productDetail);

            await Repository.InsertAsync(productDetail, autoSave: true);

            return await MapToGetOutputDtoAsync(productDetail);
        }

        public override async Task<ProductDetailDto> UpdateAsync(Guid id, CreateUpdateProductDetailDto input)
        {
            await CheckUpdatePolicyAsync();

            var product = await _productRepository.GetAsync(x => x.ProductDetailId == id);

            await AuthorizationService.CheckMultiStorePolicyAsync(product.StoreId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);
            
            if (input.StoreId != product.StoreId)
            {
                await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId, UpdatePolicyName,
                    ProductsPermissions.Products.CrossStore);
            }
            
            var detail = await GetEntityByIdAsync(id);

            MapToEntity(input, detail);
            
            await Repository.UpdateAsync(detail, autoSave: true);

            return await MapToGetOutputDtoAsync(detail);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await CheckDeletePolicyAsync();
            
            var product = await _productRepository.GetAsync(x => x.ProductDetailId == id);

            await AuthorizationService.CheckMultiStorePolicyAsync(product.StoreId, DeletePolicyName,
                ProductsPermissions.Products.CrossStore);

            await Repository.DeleteAsync(id);
        }
    }
}
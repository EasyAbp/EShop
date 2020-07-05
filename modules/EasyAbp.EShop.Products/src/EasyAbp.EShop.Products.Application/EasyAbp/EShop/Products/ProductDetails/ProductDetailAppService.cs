using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.ProductStores;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public class ProductDetailAppService : CrudAppService<ProductDetail, ProductDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductDetailDto, CreateUpdateProductDetailDto>,
        IProductDetailAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Products.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Products.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Products.Update;
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Default;

        private readonly IProductRepository _productRepository;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IProductDetailRepository _repository;

        public ProductDetailAppService(
            IProductRepository productRepository,
            IProductStoreRepository productStoreRepository,
            IProductDetailRepository repository) : base(repository)
        {
            _productRepository = productRepository;
            _productStoreRepository = productStoreRepository;
            _repository = repository;
        }

        public override async Task<ProductDetailDto> UpdateAsync(Guid id, CreateUpdateProductDetailDto input)
        {
            await CheckUpdatePolicyAsync();

            var product = await _productRepository.GetAsync(x => x.ProductDetailId == id);
            
            await CheckStoreIsProductOwnerAsync(product.Id, input.StoreId);
            
            var detail = await GetEntityByIdAsync(id);

            MapToEntity(input, detail);
            
            await Repository.UpdateAsync(detail, autoSave: true);

            return MapToGetOutputDto(detail);
        }

        public virtual async Task DeleteAsync(Guid id, Guid storeId)
        {
            await CheckDeletePolicyAsync();
            
            var product = await _productRepository.GetAsync(x => x.ProductDetailId == id);

            await CheckStoreIsProductOwnerAsync(product.Id, storeId);

            await Repository.DeleteAsync(id);
        }
        
        [Obsolete("Should use DeleteAsync(Guid id, Guid storeId)")]
        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            throw new NotSupportedException();
        }

        protected virtual async Task CheckStoreIsProductOwnerAsync(Guid productId, Guid storeId)
        {
            var productStore = await _productStoreRepository.GetAsync(productId, storeId);

            if (!productStore.IsOwner)
            {
                throw new StoreIsNotProductOwnerException(productId, storeId);
            }
        }
    }
}
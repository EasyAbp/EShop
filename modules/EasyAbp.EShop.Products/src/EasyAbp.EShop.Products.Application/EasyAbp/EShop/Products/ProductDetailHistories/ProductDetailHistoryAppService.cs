using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Localization;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductDetailHistories.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistoryAppService : ReadOnlyAppService<ProductDetailHistory, ProductDetailHistoryDto, Guid, GetProductDetailHistoryListDto>,
        IProductDetailHistoryAppService
    {
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Manage;
        
        private readonly IProductDetailHistoryRepository _repository;

        public ProductDetailHistoryAppService(IProductDetailHistoryRepository repository) : base(repository)
        {
            _repository = repository;

            LocalizationResource = typeof(ProductsResource);
            ObjectMapperContext = typeof(EShopProductsApplicationModule);
        }

        protected override async Task<IQueryable<ProductDetailHistory>> CreateFilteredQueryAsync(GetProductDetailHistoryListDto input)
        {
            return (await base.CreateFilteredQueryAsync(input)).Where(x => x.ProductDetailId == input.ProductDetailId);
        }

        public async Task<ProductDetailHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime)
        {
            await CheckGetPolicyAsync();
            
            return await MapToGetOutputDtoAsync(await _repository.GetAsync(productId, modificationTime));
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.ProductDetailHistories.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistoryAppService : CrudAppService<ProductDetailHistory, ProductDetailHistoryDto, Guid, GetProductDetailHistoryListDto, object, object>,
        IProductDetailHistoryAppService
    {
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Default;
        
        private readonly IProductDetailHistoryRepository _repository;

        public ProductDetailHistoryAppService(IProductDetailHistoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override IQueryable<ProductDetailHistory> CreateFilteredQuery(GetProductDetailHistoryListDto input)
        {
            return base.CreateFilteredQuery(input).Where(x => x.ProductDetailId == input.ProductDetailId);
        }

        public async Task<ProductDetailHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime)
        {
            await CheckGetPolicyAsync();
            
            return MapToGetOutputDto(await _repository.GetAsync(productId, modificationTime));
        }

        [RemoteService(false)]
        public override Task<ProductDetailHistoryDto> CreateAsync(object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task<ProductDetailHistoryDto> UpdateAsync(Guid id, object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            throw new NotSupportedException();
        }
    }
}
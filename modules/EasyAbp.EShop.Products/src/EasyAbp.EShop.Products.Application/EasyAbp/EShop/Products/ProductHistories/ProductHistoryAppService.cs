using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.ProductHistories.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public class ProductHistoryAppService : CrudAppService<ProductHistory, ProductHistoryDto, Guid, GetProductHistoryListDto, object, object>,
        IProductHistoryAppService
    {
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Default;

        private readonly IProductHistoryRepository _repository;

        public ProductHistoryAppService(IProductHistoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override IQueryable<ProductHistory> CreateFilteredQuery(GetProductHistoryListDto input)
        {
            return base.CreateFilteredQuery(input).Where(x => x.ProductId == input.ProductId);
        }

        public async Task<ProductHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime)
        {
            await CheckGetPolicyAsync();
            
            return MapToGetOutputDto(await _repository.GetAsync(productId, modificationTime));
        }

        [RemoteService(false)]
        public override Task<ProductHistoryDto> CreateAsync(object input)
        {
            throw new NotImplementedException();
        }

        [RemoteService(false)]
        public override Task<ProductHistoryDto> UpdateAsync(Guid id, object input)
        {
            throw new NotImplementedException();
        }

        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
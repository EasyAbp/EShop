using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductHistories.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public class ProductHistoryAppService : ReadOnlyAppService<ProductHistory, ProductHistoryDto, Guid, GetProductHistoryListDto>,
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
    }
}
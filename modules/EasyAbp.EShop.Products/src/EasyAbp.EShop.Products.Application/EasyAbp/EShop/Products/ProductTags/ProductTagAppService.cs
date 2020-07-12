using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductTags.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.ProductTags
{
    public class ProductTagAppService : CrudAppService<ProductTag, ProductTagDto, Guid, GetProductTagListDto, object, object>,
        IProductTagAppService
    {
        public ProductTagAppService(IRepository<ProductTag, Guid> repository) : base(repository)
        {
        }

        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Default;

        protected override IQueryable<ProductTag> CreateFilteredQuery(GetProductTagListDto input)
        {
            var queryable = Repository.AsQueryable();

            if (input.TagId.HasValue)
            {
                queryable = queryable.Where(x => x.TagId == input.TagId);
            }

            if (input.ProductId.HasValue)
            {
                queryable = queryable.Where(x => x.ProductId == input.ProductId);
            }

            return queryable;
        }

        [RemoteService(false)]
        public override Task<ProductTagDto> GetAsync(Guid id)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task<ProductTagDto> CreateAsync(object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task<ProductTagDto> UpdateAsync(Guid id, object input)
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

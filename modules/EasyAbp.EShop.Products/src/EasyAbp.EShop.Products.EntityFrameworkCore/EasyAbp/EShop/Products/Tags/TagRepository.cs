using EasyAbp.Abp.Trees;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Tags
{
    public class TagRepository : EfCoreTreeRepository<ProductsDbContext, Tag>, ITagRepository
    {
        public TagRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Tag>> GetListByAsync(Guid storeId, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().Where(x => x.StoreId == storeId).ToListAsync(GetCancellationToken(cancellationToken))
                : await DbSet.Where(x => x.StoreId == storeId).ToListAsync(GetCancellationToken(cancellationToken));
        }

        [Obsolete("Should use GetListByAsync(Guid storeId, bool includeDetails, CancellationToken cancellationToken)")]
        public override Task<List<Tag>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = new CancellationToken())
        {
            return base.GetListAsync(includeDetails, cancellationToken);
        }
    }
}

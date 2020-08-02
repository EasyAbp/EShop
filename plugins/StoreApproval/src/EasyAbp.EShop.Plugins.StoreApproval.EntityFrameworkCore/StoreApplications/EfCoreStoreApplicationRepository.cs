using EasyAbp.EShop.Plugins.StoreApproval.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    public class EfCoreStoreApplicationRepository : EfCoreRepository<StoreApprovalDbContext, StoreApplication, Guid>, IStoreApplicationRepository
    {
        public EfCoreStoreApplicationRepository(IDbContextProvider<StoreApprovalDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}

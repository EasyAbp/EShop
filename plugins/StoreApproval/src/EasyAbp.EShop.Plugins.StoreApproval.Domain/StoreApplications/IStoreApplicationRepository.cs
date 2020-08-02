using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    public interface IStoreApplicationRepository : IRepository<StoreApplication, Guid>
    {
    }
}

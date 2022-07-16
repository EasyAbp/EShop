using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores
{
    public interface IGrantedStoreRepository : IRepository<GrantedStore, Guid>
    {
    }
}
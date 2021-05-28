using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public interface IOutstockRepository : IRepository<Outstock, Guid>
    {
    }
}
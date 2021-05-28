using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public interface IInstockRepository : IRepository<Instock, Guid>
    {
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public interface IWarehouseRepository : IRepository<Warehouse, Guid>
    {
        Task<Warehouse> FindDefaultWarehouseAsync(Guid storeId, CancellationToken cancellationToken = default);
    }
}
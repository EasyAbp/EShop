using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public class WarehouseRepository : EfCoreRepository<IInventoryDbContext, Warehouse, Guid>, IWarehouseRepository
    {
        public WarehouseRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Warehouse> FindDefaultWarehouseAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            return await(await WithDetailsAsync()).Where(w => w.StoreId == storeId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
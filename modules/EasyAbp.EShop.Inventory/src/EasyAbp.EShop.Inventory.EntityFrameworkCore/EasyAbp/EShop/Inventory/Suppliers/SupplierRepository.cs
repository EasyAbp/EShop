using System;
using EasyAbp.EShop.Inventory.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Suppliers
{
    public class SupplierRepository : EfCoreRepository<IInventoryDbContext, Supplier, Guid>, ISupplierRepository
    {
        public SupplierRepository(IDbContextProvider<IInventoryDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
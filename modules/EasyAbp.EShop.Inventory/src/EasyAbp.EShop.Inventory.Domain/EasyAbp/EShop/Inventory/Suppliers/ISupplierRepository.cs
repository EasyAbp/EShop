using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Inventory.Suppliers
{
    public interface ISupplierRepository : IRepository<Supplier, Guid>
    {
    }
}
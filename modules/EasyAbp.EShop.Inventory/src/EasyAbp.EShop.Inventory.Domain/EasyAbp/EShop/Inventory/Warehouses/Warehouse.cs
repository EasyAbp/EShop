using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Inventories;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Reallocations;
using EasyAbp.EShop.Inventory.StockHistories;
using EasyAbp.EShop.Inventory.Stocks;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Address = EasyAbp.EShop.Inventory.Inventories.Address;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public class Warehouse : FullAuditedAggregateRoot<Guid>, IWarehouse, IMultiTenant
    {
        public virtual string Name { get; protected set; }

        public virtual Address Address { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual string ContactName { get; protected set; }

        public virtual string ContactPhoneNumber { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        public virtual Guid? TenantId { get; protected set; }

        public virtual List<Stock> Stocks { get; protected set; }

        public virtual List<StockHistory> StockHistories { get; protected set; }

        public virtual List<Instock> Instocks { get; protected set; }

        public virtual List<Outstock> Outstocks { get; protected set; }

        public virtual List<Reallocation> Reallocations { get; protected set; }

        protected Warehouse()
        {
        }

        public Warehouse(
            Guid id,
            string name,
            Address address,
            string description,
            string contactName,
            string contactPhoneNumber,
            Guid storeId,
            Guid? tenantId
        ) : base(id)
        {
            Name = name;
            Address = address;
            Description = description;
            ContactName = contactName;
            ContactPhoneNumber = contactPhoneNumber;
            StoreId = storeId;
            TenantId = tenantId;
        }

        public Warehouse(
            Guid id,
            string name,
            Address address,
            string description,
            string contactName,
            string contactPhoneNumber,
            Guid storeId,
            Guid? tenantId,
            List<Stock> stocks,
            List<StockHistory> stockHistories,
            List<Instock> instocks,
            List<Outstock> outstocks,
            List<Reallocation> reallocations
        ) : base(id)
        {
            Name = name;
            Address = address;
            Description = description;
            ContactName = contactName;
            ContactPhoneNumber = contactPhoneNumber;
            StoreId = storeId;
            TenantId = tenantId;
            Stocks = stocks;
            StockHistories = stockHistories;
            Instocks = instocks;
            Outstocks = outstocks;
            Reallocations = reallocations;
        }
    }
}
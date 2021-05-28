using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Inventory.Suppliers.Dtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Suppliers
{
    public class SupplierAppService : CrudAppService<Supplier, SupplierDto, Guid, PagedAndSortedResultRequestDto, SupplierCreateDto, SupplierUpdateDto>,
        ISupplierAppService
    {
        protected override string GetPolicyName { get; set; } = InventoryPermissions.Supplier.Default;
        protected override string GetListPolicyName { get; set; } = InventoryPermissions.Supplier.Default;
        protected override string CreatePolicyName { get; set; } = InventoryPermissions.Supplier.Create;
        protected override string UpdatePolicyName { get; set; } = InventoryPermissions.Supplier.Update;
        protected override string DeletePolicyName { get; set; } = InventoryPermissions.Supplier.Delete;

        private readonly ISupplierRepository _repository;

        public SupplierAppService(ISupplierRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
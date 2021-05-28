using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Inventory.Reallocations.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Reallocations
{
    public class ReallocationAppService : MultiStoreCrudAppService<Reallocation, ReallocationDto, Guid, GetReallocationListInput, ReallocationCreateDto, ReallocationUpdateDto>,
        IReallocationAppService
    {
        protected override string GetPolicyName { get; set; } = InventoryPermissions.Stock.Default;
        protected override string GetListPolicyName { get; set; } = InventoryPermissions.Stock.Default;
        protected override string CreatePolicyName { get; set; } = InventoryPermissions.Stock.Create;
        protected override string UpdatePolicyName { get; set; } = InventoryPermissions.Stock.Update;
        protected override string DeletePolicyName { get; set; } = InventoryPermissions.Stock.Delete;
        protected override string CrossStorePolicyName { get; set; } = InventoryPermissions.Stock.CrossStore;

        private readonly IReallocationRepository _repository;
        
        public ReallocationAppService(IReallocationRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<Reallocation>> CreateFilteredQueryAsync(GetReallocationListInput input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(!input.Filter.IsNullOrEmpty(), s => s.Description.Contains(input.Filter))
                .WhereIf(input.ProductSkuId.HasValue, s => s.ProductSkuId == input.ProductSkuId.Value)
                .WhereIf(input.SourceWarehouseId.HasValue, s => s.SourceWarehouseId == input.SourceWarehouseId.Value)
                .WhereIf(input.DestinationWarehouseId.HasValue, s => s.DestinationWarehouseId == input.SourceWarehouseId.Value)
                .WhereIf(input.StoreId.HasValue, s => s.StoreId == input.StoreId.Value)
                .WhereIf(input.CreationStartTime.HasValue, s => s.CreationTime.Date >= input.CreationStartTime.Value.Date)
                .WhereIf(input.CreationEndTime.HasValue, s => s.CreationTime.Date >= input.CreationEndTime.Value.Date)
                .WhereIf(input.ReallocationStartTime.HasValue, s => s.ReallocationTime.Date >= input.ReallocationStartTime.Value.Date)
                .WhereIf(input.ReallocationEndTime.HasValue, s => s.ReallocationTime.Date >= input.ReallocationEndTime.Value.Date);
        }
    }
}

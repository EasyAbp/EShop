using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Inventory.StockHistories.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.StockHistories
{
    public class StockHistoryAppService : MultiStoreCrudAppService<StockHistory, StockHistoryDto, Guid, GetStockHistoryListInput, StockHistoryCreateDto, StockHistoryUpdateDto>,
        IStockHistoryAppService
    {
        protected override string GetPolicyName { get; set; } = InventoryPermissions.Stock.Default;
        protected override string GetListPolicyName { get; set; } = InventoryPermissions.Stock.Default;
        protected override string CreatePolicyName { get; set; } = InventoryPermissions.Stock.Create;
        protected override string UpdatePolicyName { get; set; } = InventoryPermissions.Stock.Update;
        protected override string DeletePolicyName { get; set; } = InventoryPermissions.Stock.Delete;
        protected override string CrossStorePolicyName { get; set; } = InventoryPermissions.Stock.CrossStore;

        private readonly IStockHistoryRepository _repository;
        
        public StockHistoryAppService(IStockHistoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<StockHistory>> CreateFilteredQueryAsync(GetStockHistoryListInput input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(!input.Filter.IsNullOrEmpty(), s => s.Description.Contains(input.Filter))
                .WhereIf(input.ProductSkuId.HasValue, s => s.ProductSkuId == input.ProductSkuId.Value)
                .WhereIf(input.WarehouseId.HasValue, s => s.WarehouseId == input.WarehouseId.Value)
                .WhereIf(input.StoreId.HasValue, s => s.StoreId == input.StoreId.Value)
                .WhereIf(input.CreationStartTime.HasValue, s => s.CreationTime >= input.CreationStartTime)
                .WhereIf(input.CreationEndTime.HasValue, s => s.CreationTime <= input.CreationEndTime);
        }

    }
}

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Inventory.Stocks.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public class StockAppService : MultiStoreCrudAppService<Stock, StockDto, Guid, GetStockListInput, StockCreateDto, StockUpdateDto>,
        IStockAppService
    {
        protected override string GetPolicyName { get; set; } = InventoryPermissions.Stock.Default;
        protected override string GetListPolicyName { get; set; } = InventoryPermissions.Stock.Default;
        protected override string CreatePolicyName { get; set; } = InventoryPermissions.Stock.Create;
        protected override string UpdatePolicyName { get; set; } = InventoryPermissions.Stock.Update;
        protected override string DeletePolicyName { get; set; } = InventoryPermissions.Stock.Delete;
        protected override string CrossStorePolicyName { get; set; } = InventoryPermissions.Stock.CrossStore;

        private readonly IStockRepository _repository;
        private readonly IStockManager _stockManager;
        public StockAppService(IStockRepository repository, IStockManager stockManager) : base(repository)
        {
            _repository = repository;
            _stockManager = stockManager;
        }

        public override async Task<StockDto> CreateAsync(StockCreateDto input)
        {
            CurrentUnitOfWork.OnCompleted(async () =>
            {
                await _stockManager.AdjustStock(input.ProductSkuId, input.WarehouseId, 0, "初始化库存");
            });

            return await base.CreateAsync(input);
        }

        protected override async Task<IQueryable<Stock>> CreateFilteredQueryAsync(GetStockListInput input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(!input.Filter.IsNullOrEmpty(), s => s.Description.Contains(input.Filter))
                .WhereIf(input.ProductSkuId.HasValue, s => s.ProductSkuId == input.ProductSkuId.Value)
                .WhereIf(input.WarehouseId.HasValue, s => s.WarehouseId == input.WarehouseId.Value)
                .WhereIf(input.StoreId.HasValue, s => s.StoreId == input.StoreId.Value)
                .WhereIf(input.IsEnabled.HasValue, s => s.IsEnabled == input.IsEnabled)
                .WhereIf(input.CreationStartTime.HasValue, s => s.CreationTime.Date >= input.CreationStartTime.Value.Date)
                .WhereIf(input.CreationEndTime.HasValue, s => s.CreationTime.Date >= input.CreationEndTime.Value.Date);
        }

    }
}

using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Inventory.Inventories
{
    public class CreateProductSkuStockEventHandler : IDistributedEventHandler<CreateProductSkuStockEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IStockRepository _stockRepository;
        private readonly IStockManager _stockManager;
        private readonly IWarehouseManager _warehouseManager;

        public CreateProductSkuStockEventHandler(
            ICurrentTenant currentTenant,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IStockRepository stockRepository,
            IStockManager stockManager,
            IWarehouseManager warehouseManager)
        {
            _currentTenant = currentTenant;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _stockRepository = stockRepository;
            _warehouseManager = warehouseManager;
            _stockManager = stockManager;
        }

        public virtual async Task HandleEventAsync(CreateProductSkuStockEto eventData)
        {

            if (eventData.WarehouseIds.IsNullOrEmpty())
            {
                var warehouse = await _warehouseManager.GetDefaultWarehouse(eventData.StoreId);

                eventData.WarehouseIds = new List<Guid> { warehouse.Id };
            }

            foreach (var warehouseId in eventData.WarehouseIds)
            {
                var exist = _stockRepository.Where(s => s.WarehouseId == warehouseId && s.ProductSkuId == eventData.ProductSkuId).Count() > 0;

                if (exist)
                {
                    continue;
                }

                var stock = _objectMapper.Map<CreateProductSkuStockEto, Stock>(eventData);

                stock.SetWarehouseId(warehouseId);

                await _stockRepository.InsertAsync(stock);

            }

        }
    }
}
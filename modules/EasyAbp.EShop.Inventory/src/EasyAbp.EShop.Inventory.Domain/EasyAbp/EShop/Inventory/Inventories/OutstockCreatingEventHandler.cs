using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Stocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace EasyAbp.EShop.Inventory.Inventories
{
    public class OutstockCreatingEventHandler : ILocalEventHandler<EntityCreatedEventData<Outstock>>,
          ITransientDependency
    {

        private readonly IStockManager _stockManager;

        public OutstockCreatingEventHandler(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<Outstock> eventData)
        {
            await _stockManager.AdjustStock(eventData.Entity.ProductSkuId, eventData.Entity.WarehouseId, -eventData.Entity.Units, "商品出库");
        }
    }
}

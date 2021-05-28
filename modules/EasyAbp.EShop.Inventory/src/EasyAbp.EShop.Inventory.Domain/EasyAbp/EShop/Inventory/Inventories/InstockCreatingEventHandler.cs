using EasyAbp.EShop.Inventory.Instocks;
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
    public class InstockCreatingEventHandler : ILocalEventHandler<EntityCreatedEventData<Instock>>,
          ITransientDependency
    {

        private readonly IStockManager _stockManager;

        public InstockCreatingEventHandler(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<Instock> eventData)
        {
            await _stockManager.AdjustStock(eventData.Entity.ProductSkuId, eventData.Entity.WarehouseId, eventData.Entity.Units, "商品入库");
        }
    }
}

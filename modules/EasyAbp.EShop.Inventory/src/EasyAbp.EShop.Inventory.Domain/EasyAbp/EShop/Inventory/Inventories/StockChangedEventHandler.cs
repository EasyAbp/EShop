using EasyAbp.EShop.Inventory.StockHistories;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;

namespace EasyAbp.EShop.Inventory.Inventories
{
    public class StockChangedEventHandler
    : ILocalEventHandler<StockChangedEto>,
      ITransientDependency
    {
        private readonly IStockHistoryRepository _stockHistoryRepository;
        private readonly IGuidGenerator _guidGenerator;

        public StockChangedEventHandler(
            IStockHistoryRepository stockHistoryRepository,
            IGuidGenerator guidGenerator
            )
        {
            _stockHistoryRepository = stockHistoryRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task HandleEventAsync(StockChangedEto eventData)
        {
            await _stockHistoryRepository.InsertAsync(
                new StockHistory(
                    _guidGenerator.Create(),
                    eventData.TenantId,
                    eventData.LockedQuantity,
                    eventData.ProductSkuId,
                    eventData.ProductId,
                    eventData.Quantity,
                    eventData.AdjustedQuantity,
                    eventData.Description,
                    eventData.WarehouseId,
                    eventData.StoreId
                    ));
        }
    }
}
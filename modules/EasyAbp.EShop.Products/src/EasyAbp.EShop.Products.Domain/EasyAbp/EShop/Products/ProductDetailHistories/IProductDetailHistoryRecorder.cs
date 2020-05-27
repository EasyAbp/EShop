using EasyAbp.EShop.Products.ProductDetails;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public interface IProductDetailHistoryRecorder : ILocalEventHandler<EntityChangedEventData<ProductDetail>>
    {
        
    }
}
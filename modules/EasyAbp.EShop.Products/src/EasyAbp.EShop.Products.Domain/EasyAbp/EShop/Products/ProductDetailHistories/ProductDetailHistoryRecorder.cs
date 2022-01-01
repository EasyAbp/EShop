using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistoryRecorder :
        IProductDetailHistoryRecorder,
        ILocalEventHandler<EntityChangedEventData<ProductDetail>>,
        ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProductDetailHistoryRepository _productDetailHistoryRepository;

        public ProductDetailHistoryRecorder(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IJsonSerializer jsonSerializer,
            IProductDetailHistoryRepository productDetailHistoryRepository)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _jsonSerializer = jsonSerializer;
            _productDetailHistoryRepository = productDetailHistoryRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityChangedEventData<ProductDetail> eventData)
        {
            var modificationTime = eventData.Entity.LastModificationTime ?? eventData.Entity.CreationTime;

            var serializeEntityData = _jsonSerializer.Serialize(eventData.Entity);

            await _productDetailHistoryRepository.InsertAsync(new ProductDetailHistory(_guidGenerator.Create(),
                _currentTenant.Id, eventData.Entity.Id, modificationTime, serializeEntityData));
        }
    }
}
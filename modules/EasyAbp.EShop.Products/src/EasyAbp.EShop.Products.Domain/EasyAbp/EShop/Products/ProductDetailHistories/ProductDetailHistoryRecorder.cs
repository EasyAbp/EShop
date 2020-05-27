using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistoryRecorder : IProductDetailHistoryRecorder, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProductDetailHistoryRepository _productDetailHistoryRepository;

        public ProductDetailHistoryRecorder(
            IGuidGenerator guidGenerator,
            IJsonSerializer jsonSerializer,
            IProductDetailHistoryRepository productDetailHistoryRepository)
        {
            _guidGenerator = guidGenerator;
            _jsonSerializer = jsonSerializer;
            _productDetailHistoryRepository = productDetailHistoryRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityChangedEventData<ProductDetail> eventData)
        {
            var modificationTime = eventData.Entity.LastModificationTime ?? eventData.Entity.CreationTime;

            var serializeEntityData = _jsonSerializer.Serialize(eventData.Entity);

            await _productDetailHistoryRepository.InsertAsync(new ProductDetailHistory(_guidGenerator.Create(),
                eventData.Entity.Id, modificationTime, serializeEntityData));
        }
    }
}
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.ProductHistories;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistoryRecorder : ILocalEventHandler<EntityChangedEventData<ProductDetail>>, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IObjectMapper _objectMapper;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProductDetailHistoryRepository _productDetailHistoryRepository;

        public ProductDetailHistoryRecorder(
            IGuidGenerator guidGenerator,
            IObjectMapper objectMapper,
            IJsonSerializer jsonSerializer,
            IProductDetailHistoryRepository productDetailHistoryRepository)
        {
            _guidGenerator = guidGenerator;
            _objectMapper = objectMapper;
            _jsonSerializer = jsonSerializer;
            _productDetailHistoryRepository = productDetailHistoryRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityChangedEventData<ProductDetail> eventData)
        {
            var modificationTime = eventData.Entity.LastModificationTime ?? eventData.Entity.CreationTime;

            var serializedDto =
                _jsonSerializer.Serialize(_objectMapper.Map<ProductDetail, ProductDetailDto>(eventData.Entity));

            await _productDetailHistoryRepository.InsertAsync(new ProductDetailHistory(_guidGenerator.Create(),
                eventData.Entity.Id, modificationTime, serializedDto));
        }
    }
}
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.ObjectMapping;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public class ProductHistoryRecorder : ILocalEventHandler<EntityChangedEventData<Product>>, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IObjectMapper _objectMapper;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProductHistoryRepository _productHistoryRepository;

        public ProductHistoryRecorder(
            IGuidGenerator guidGenerator,
            IObjectMapper objectMapper,
            IJsonSerializer jsonSerializer,
            IProductHistoryRepository productHistoryRepository)
        {
            _guidGenerator = guidGenerator;
            _objectMapper = objectMapper;
            _jsonSerializer = jsonSerializer;
            _productHistoryRepository = productHistoryRepository;
        }
        
        public async Task HandleEventAsync(EntityChangedEventData<Product> eventData)
        {
            var modificationTime = eventData.Entity.LastModificationTime ?? eventData.Entity.CreationTime;

            var serializedDto = _jsonSerializer.Serialize(_objectMapper.Map<Product, ProductDto>(eventData.Entity));

            await _productHistoryRepository.InsertAsync(new ProductHistory(_guidGenerator.Create(), eventData.Entity.Id,
                modificationTime, serializedDto));
        }
    }
}
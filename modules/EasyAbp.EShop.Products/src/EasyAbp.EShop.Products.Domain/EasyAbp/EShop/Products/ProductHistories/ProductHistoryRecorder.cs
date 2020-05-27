﻿using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public class ProductHistoryRecorder : IProductHistoryRecorder, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProductHistoryRepository _productHistoryRepository;

        public ProductHistoryRecorder(
            IGuidGenerator guidGenerator,
            IJsonSerializer jsonSerializer,
            IProductHistoryRepository productHistoryRepository)
        {
            _guidGenerator = guidGenerator;
            _jsonSerializer = jsonSerializer;
            _productHistoryRepository = productHistoryRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityChangedEventData<Product> eventData)
        {
            var modificationTime = eventData.Entity.LastModificationTime ?? eventData.Entity.CreationTime;

            var serializedEntityData = _jsonSerializer.Serialize(eventData.Entity);

            await _productHistoryRepository.InsertAsync(new ProductHistory(_guidGenerator.Create(), eventData.Entity.Id,
                modificationTime, serializedEntityData));
        }
    }
}
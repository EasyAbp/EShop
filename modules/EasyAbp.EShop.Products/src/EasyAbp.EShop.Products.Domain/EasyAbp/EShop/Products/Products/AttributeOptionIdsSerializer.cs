using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Products.Products
{
    public class AttributeOptionIdsSerializer : IAttributeOptionIdsSerializer, ITransientDependency
    {
        private readonly IJsonSerializer _jsonSerializer;

        public AttributeOptionIdsSerializer(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }
        
        public async Task<string> FormatAsync(string serializedAttributeOptionIds)
        {
            return await SerializeAsync(await DeserializeAsync(serializedAttributeOptionIds));
        }

        public Task<string> SerializeAsync(IEnumerable<Guid> attributeOptionIds)
        {
            return Task.FromResult(_jsonSerializer.Serialize(attributeOptionIds.OrderBy(x => x)));
        }

        public Task<IEnumerable<Guid>> DeserializeAsync(string serializedAttributeOptionIds)
        {
            return Task.FromResult(_jsonSerializer.Deserialize<IEnumerable<Guid>>(serializedAttributeOptionIds));
        }
    }
}
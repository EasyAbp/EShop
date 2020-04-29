using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    public class AttributeOptionIdsSerializer : IAttributeOptionIdsSerializer, ITransientDependency
    {
        public async Task<string> FormatAsync(string serializedAttributeOptionIds)
        {
            return await SerializeAsync(await DeserializeAsync(serializedAttributeOptionIds));
        }

        public Task<string> SerializeAsync(IEnumerable<Guid> attributeOptionIds)
        {
            return Task.FromResult(JsonConvert.SerializeObject(attributeOptionIds.OrderBy(x => x)));
        }

        public Task<IEnumerable<Guid>> DeserializeAsync(string serializedAttributeOptionIds)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<Guid>>(serializedAttributeOptionIds));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    public class SerializedAttributeOptionIdsFormatter : ISerializedAttributeOptionIdsFormatter, ITransientDependency
    {
        public async Task<string> ParseAsync(string serializedAttributeOptionIds)
        {
            return await ParseAsync(JsonConvert.DeserializeObject<IEnumerable<Guid>>(serializedAttributeOptionIds));
        }

        public Task<string> ParseAsync(IEnumerable<Guid> attributeOptionIds)
        {
            return Task.FromResult(JsonConvert.SerializeObject(attributeOptionIds.OrderBy(x => x)));
        }
    }
}
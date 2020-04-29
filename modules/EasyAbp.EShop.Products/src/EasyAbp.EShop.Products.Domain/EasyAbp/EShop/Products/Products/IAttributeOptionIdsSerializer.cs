using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IAttributeOptionIdsSerializer
    {
        Task<string> FormatAsync(string serializedAttributeOptionIds);
        
        Task<string> SerializeAsync(IEnumerable<Guid> attributeOptionIds);
        
        Task<IEnumerable<Guid>> DeserializeAsync(string serializedAttributeOptionIds);
    }
}
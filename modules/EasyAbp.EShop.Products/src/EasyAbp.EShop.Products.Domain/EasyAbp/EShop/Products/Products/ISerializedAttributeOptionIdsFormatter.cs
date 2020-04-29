using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface ISerializedAttributeOptionIdsFormatter
    {
        Task<string> ParseAsync(string serializedAttributeOptionIds);
        
        Task<string> ParseAsync(IEnumerable<Guid> attributeOptionIds);
    }
}
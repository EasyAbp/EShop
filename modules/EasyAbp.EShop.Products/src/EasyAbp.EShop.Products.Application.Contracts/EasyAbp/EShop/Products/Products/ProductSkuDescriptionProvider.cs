using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuDescriptionProvider : IProductSkuDescriptionProvider, ITransientDependency
    {
        private readonly IJsonSerializer _jsonSerializer;

        public ProductSkuDescriptionProvider(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }
        
        public virtual Task<string> GenerateAsync(ProductDto productDto, ProductSkuDto productSkuDto)
        {
            var names = new Collection<string[]>();

            foreach (var attributeOptionId in productSkuDto.AttributeOptionIds)
            {
                names.Add(productDto.ProductAttributes.SelectMany(
                    attribute => attribute.ProductAttributeOptions.Where(option => option.Id == attributeOptionId),
                    (attribute, option) => new [] {attribute.DisplayName, option.DisplayName}).Single());
            }
            
            return Task.FromResult(_jsonSerializer.Serialize(names));
        }
    }
}
using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.ObjectMapping;

namespace EasyAbp.EShop.Products.Products;

public class FakeProductCache : IProductCache
{
    protected IProductAppService ProductAppService { get; }
    protected IObjectMapper ObjectMapper { get; }

    public FakeProductCache(IProductAppService productAppService, IObjectMapper objectMapper)
    {
        ProductAppService = productAppService;
        ObjectMapper = objectMapper;
    }

    public async Task<ProductCacheItem> GetAsync(Guid productId)
    {
        var dto = await ProductAppService.GetAsync(productId);
        return ObjectMapper.Map<ProductDto, ProductCacheItem>(dto);
    }

    public Task RemoveAsync(Guid productId)
    {
        return Task.CompletedTask;
    }
}

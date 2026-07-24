using System;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public class ProductDetailMapperlyTests : ProductsApplicationTestBase
    {
        private readonly IObjectMapper<EShopProductsApplicationModule> _objectMapper;

        public ProductDetailMapperlyTests()
        {
            _objectMapper = GetRequiredService<IObjectMapper<EShopProductsApplicationModule>>();
        }

        [Fact]
        public void Should_Map_ProductDetail_To_ProductDetailDto()
        {
            var storeId = Guid.NewGuid();
            var entity = new ProductDetail(Guid.NewGuid(), null, storeId, "A description");

            var dto = _objectMapper.Map<ProductDetail, ProductDetailDto>(entity);

            dto.ShouldNotBeNull();
            dto.Id.ShouldBe(entity.Id);
            dto.StoreId.ShouldBe(storeId);
            dto.Description.ShouldBe("A description");
        }
    }
}

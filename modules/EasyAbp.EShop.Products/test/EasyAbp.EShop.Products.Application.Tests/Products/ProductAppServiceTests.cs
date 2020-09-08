using System.Collections.Generic;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Options;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.Options;
using Xunit;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAppServiceTests : ProductsApplicationTestBase
    {
        private readonly IProductAppService _productAppService;
        private readonly EShopProductsOptions _eShopProductsOptions;

        public ProductAppServiceTests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
            _eShopProductsOptions = GetRequiredService<IOptions<EShopProductsOptions>>().Value;
        }

        [Fact]
        public async Task Should_Create_A_Product_With_SKU()
        {
            // Arrange
            _eShopProductsOptions.Groups.Configure("Default Group Name", x =>
            {
                x.DisplayName = "Default Group Name";
                x.Description = "Default Description";
            });

            var requestDto = new CreateUpdateProductDto
            {
                ProductGroupName = "Default Group Name",
                ProductDetailId = ProductsTestData.ProductDetails1Id,
                StoreId = ProductsTestData.Store1Id,
                UniqueName = "Unique Pencil",
                DisplayName = "Pencil",
                InventoryStrategy = InventoryStrategy.NoNeed,
                DisplayOrder = 0,
                IsPublished = true,
                ProductAttributes = new List<CreateUpdateProductAttributeDto>
                {
                    new CreateUpdateProductAttributeDto
                    {
                        DisplayName = "Default Attribute 1",
                        Description = "Default Description 1",
                        DisplayOrder = 1,
                        ProductAttributeOptions = new List<CreateUpdateProductAttributeOptionDto>
                        {
                            new CreateUpdateProductAttributeOptionDto
                            {
                                DisplayName = "Option?"
                            }
                        }
                    }
                }
            };

            // Act
            var response = await _productAppService.CreateAsync(requestDto);

            // Assert
            response.ShouldNotBeNull();
            response.DisplayName.ShouldBe("Pencil");
            response.UniqueName.ShouldBe("Unique Pencil");

            UsingDbContext(db =>
            {
                var product = db.Products.FirstOrDefault(x => x.Id == response.Id);
                product.ShouldNotBeNull();
                product.DisplayName.ShouldBe("Pencil");
            });
        }
    }
}
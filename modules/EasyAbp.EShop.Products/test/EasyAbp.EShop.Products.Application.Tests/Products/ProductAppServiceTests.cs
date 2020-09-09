using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Options;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.EntityFrameworkCore;
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
        public async Task Should_Create_A_Product()
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
                                DisplayName = "Option 1"
                            }
                        }
                    }
                }
            };

            // Act
            var response = await _productAppService.CreateAsync(requestDto);

            // Assert
            response.ShouldNotBeNull();
            response.IsPublished.ShouldBe(true);
            response.DisplayName.ShouldBe("Pencil");
            response.UniqueName.ShouldBe("Unique Pencil");

            UsingDbContext(db =>
            {
                var product = db.Products.FirstOrDefault(x => x.Id == response.Id);
                product.ShouldNotBeNull();
                product.DisplayName.ShouldBe("Pencil");
            });
        }

        [Fact]
        public async Task Should_Create_A_Sku()
        {
            await Should_Create_A_Product();
            Guid productId = default;
            Guid productAttributeOptionId = default;
            UsingDbContext(db =>
            {
                var product = db.Products
                    .Include(x => x.ProductAttributes)
                    .ThenInclude(x => x.ProductAttributeOptions)
                    .FirstOrDefault(x => x.UniqueName == "Unique Pencil");
                product.ShouldNotBeNull();
                product.ProductAttributes.ShouldNotBeNull();
                product.ProductAttributes.Count.ShouldBe(1);
                var productAttribute = product.ProductAttributes.First();
                productAttribute.ProductAttributeOptions.ShouldNotBeNull();
                productAttribute.ProductAttributeOptions.Count.ShouldBe(1);
                productId = product.Id;
                productAttributeOptionId = productAttribute.ProductAttributeOptions.First().Id;
            });

            var response = await _productAppService.CreateSkuAsync(productId, ProductsTestData.Store1Id, new CreateProductSkuDto
            {
                AttributeOptionIds = new List<Guid> {productAttributeOptionId},
                Currency = "CNY",
                Price = 1m,
                OrderMinQuantity = 1,
                OrderMaxQuantity = 10
            });
            
            response.ShouldNotBeNull();
            response.MinimumPrice.ShouldBe(1m);
            response.ProductSkus.Count.ShouldBe(1);

            var responseSku = response.ProductSkus.First();
            responseSku.Currency.ShouldBe("CNY");
            responseSku.Price.ShouldBe(1m);
            responseSku.AttributeOptionIds.Count.ShouldBe(1);
            responseSku.AttributeOptionIds.First().ShouldBe(productAttributeOptionId);
            responseSku.OrderMinQuantity.ShouldBe(1);
            responseSku.OrderMaxQuantity.ShouldBe(10);
            
        }
    }
}
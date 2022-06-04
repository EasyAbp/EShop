using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Options;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Volo.Abp.Domain.Entities;
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
            
            // Arrange
            _eShopProductsOptions.Groups.Configure("Default Group Name", x =>
            {
                x.DisplayName = "Default Group Name";
                x.Description = "Default Description";
            });
        }

        [Fact]
        public async Task Should_Create_Product()
        {
            // Arrange
            var requestDto = new CreateUpdateProductDto
            {
                ProductGroupName = "Default Group Name",
                ProductDetailId = ProductsTestData.ProductDetails2Id,
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
        public async Task Should_Create_Skus()
        {
            await Should_Create_Product();
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

            var response = await _productAppService.CreateSkuAsync(productId, new CreateProductSkuDto
            {
                AttributeOptionIds = new List<Guid> {productAttributeOptionId},
                Currency = "CNY",
                Price = 1m,
                OrderMinQuantity = 1,
                OrderMaxQuantity = 10
            });
            
            response.ShouldNotBeNull();
            response.MinimumPrice.ShouldBe(1m);
            response.MaximumPrice.ShouldBe(1m);
            response.ProductSkus.Count.ShouldBe(1);

            var responseSku = response.ProductSkus.First();
            responseSku.Currency.ShouldBe("CNY");
            responseSku.Price.ShouldBe(1m);
            responseSku.AttributeOptionIds.Count.ShouldBe(1);
            responseSku.AttributeOptionIds.First().ShouldBe(productAttributeOptionId);
            responseSku.OrderMinQuantity.ShouldBe(1);
            responseSku.OrderMaxQuantity.ShouldBe(10);
        }

        [Fact]
        public async Task Should_Get_Product_Min_Max_Prices()
        {
            var getListResult = await _productAppService.GetListAsync(new GetProductListInput
            {
                StoreId = ProductsTestData.Store1Id
            });
            
            getListResult.Items.ShouldNotBeEmpty();

            var productDto = getListResult.Items.FirstOrDefault(x => x.Id == ProductsTestData.Product1Id);

            productDto.ShouldNotBeNull();
            productDto.MinimumPrice.ShouldBe(1m);
            productDto.MaximumPrice.ShouldBe(3m);
            
            var getResult = await _productAppService.GetAsync(ProductsTestData.Product1Id);

            getResult.ShouldNotBeNull();
            getResult.MinimumPrice.ShouldBe(1m);
            getResult.MaximumPrice.ShouldBe(3m);
        }
        
        [Fact]
        public async Task Should_Check_ProductDetailId()
        {
            var wrongProductDetailId = Guid.NewGuid();
            
            var requestDto = new CreateUpdateProductDto
            {
                ProductGroupName = "Default Group Name",
                StoreId = ProductsTestData.Store1Id,
                UniqueName = "Unique Pencil",
                DisplayName = "Pencil",
                ProductDetailId = wrongProductDetailId,
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
            (await Should.ThrowAsync<EntityNotFoundException>(async () =>
            {
                await _productAppService.CreateAsync(requestDto);
            })).EntityType.ShouldBe(typeof(ProductDetail));
        }
        
        [Fact]
        public async Task Should_Check_Sku_ProductDetailId()
        {
            await Should_Create_Product();

            var wrongProductDetailId = Guid.NewGuid();

            (await Should.ThrowAsync<EntityNotFoundException>(async () =>
            {
                await _productAppService.CreateSkuAsync(ProductsTestData.Product1Id, new CreateProductSkuDto
                {
                    AttributeOptionIds = new List<Guid>
                        { ProductsTestData.Product1Attribute1Option4Id, ProductsTestData.Product1Attribute2Option1Id },
                    ProductDetailId = wrongProductDetailId,
                    Currency = "CNY",
                    Price = 10m,
                    OrderMinQuantity = 1,
                    OrderMaxQuantity = 10
                });
            })).EntityType.ShouldBe(typeof(ProductDetail));
        }

        [Fact]
        public async Task Should_Get_Orderly_ProductAttributes_And_ProductAttributeOptions()
        {
            var productDto = await _productAppService.GetAsync(ProductsTestData.Product1Id);

            productDto.ProductAttributes.Count.ShouldBe(2);

            var size = productDto.ProductAttributes[0];
            var color = productDto.ProductAttributes[1];

            size.DisplayName.ShouldBe("Size");
            color.DisplayName.ShouldBe("Color");

            size.ProductAttributeOptions[0].DisplayName.ShouldBe("S");
            size.ProductAttributeOptions[1].DisplayName.ShouldBe("M");
            size.ProductAttributeOptions[2].DisplayName.ShouldBe("L");
            size.ProductAttributeOptions[3].DisplayName.ShouldBe("XL");

            color.ProductAttributeOptions[0].DisplayName.ShouldBe("Red");
            color.ProductAttributeOptions[1].DisplayName.ShouldBe("Green");
        }
    }
}
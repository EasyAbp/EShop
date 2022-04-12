using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductDomainTests : ProductsDomainTestBase
    {
        private IProductRepository ProductRepository { get; }
        private IProductManager ProductManager { get; }

        public ProductDomainTests()
        {
            ProductRepository = ServiceProvider.GetRequiredService<IProductRepository>();
            ProductManager = ServiceProvider.GetRequiredService<IProductManager>();
        }

        [Fact]
        public async Task Should_Set_ProductDetailId()
        {
            var product2 = new Product(ProductsTestData.Product2Id, null, ProductsTestData.Store1Id, "Default",
                ProductsTestData.ProductDetails2Id, "Ball", "Ball", InventoryStrategy.NoNeed, true, false, false, null,
                null, 0);

            await ProductManager.CreateAsync(product2);

            product2 = await ProductRepository.GetAsync(product2.Id);
            
            product2.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails2Id);
        }

        [Fact]
        public async Task Should_Set_Sku_ProductDetailId()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                var product1 = await ProductRepository.GetAsync(ProductsTestData.Product1Id);
                var sku1 = product1.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);
            
                sku1.ProductDetailId.ShouldBeNull();
            
                typeof(ProductSku).GetProperty(nameof(ProductSku.ProductDetailId))!.SetValue(sku1,
                    ProductsTestData.ProductDetails1Id);

                await ProductManager.UpdateAsync(product1);

                product1 = await ProductRepository.GetAsync(product1.Id);
                sku1 = product1.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

                sku1.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails1Id);
            });
        }

        [Fact]
        public async Task Should_Reuse_ProductDetail()
        {
            var product1 = await ProductRepository.GetAsync(ProductsTestData.Product1Id);

            product1.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails1Id);

            var product2 = new Product(ProductsTestData.Product2Id, null, ProductsTestData.Store1Id, "Default",
                ProductsTestData.ProductDetails2Id, "Ball", "Ball", InventoryStrategy.NoNeed, true, false, false, null,
                null, 0);

            await ProductManager.CreateAsync(product2);

            product2 = await ProductRepository.GetAsync(product2.Id);
            
            product2.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails2Id);

            typeof(Product).GetProperty(nameof(Product.ProductDetailId))!.SetValue(product2,
                ProductsTestData.ProductDetails1Id);
            
            await ProductManager.UpdateAsync(product2);
            
            product2 = await ProductRepository.GetAsync(product2.Id);

            product2.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails1Id);
        }

        [Fact]
        public async Task Should_Remove_ProductDetailId()
        {
            await Should_Set_ProductDetailId();
            
            var product2 = await ProductRepository.GetAsync(ProductsTestData.Product2Id);

            product2.ProductDetailId.ShouldNotBeNull();
            
            typeof(Product).GetProperty(nameof(Product.ProductDetailId))!.SetValue(product2, null);

            await ProductManager.UpdateAsync(product2);

            product2 = await ProductRepository.GetAsync(product2.Id);
            
            product2.ProductDetailId.ShouldBeNull();
        }

        [Fact]
        public async Task Should_Remove_Sku_ProductDetailId()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await Should_Set_Sku_ProductDetailId();

                var product1 = await ProductRepository.GetAsync(ProductsTestData.Product1Id);
                var sku1 = product1.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

                sku1.ProductDetailId.ShouldNotBeNull();

                typeof(ProductSku).GetProperty(nameof(Product.ProductDetailId))!.SetValue(sku1, null);

                await ProductManager.UpdateAsync(product1);

                product1 = await ProductRepository.GetAsync(product1.Id);
                sku1 = product1.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

                sku1.ProductDetailId.ShouldBeNull();
            });
        }
    }
}

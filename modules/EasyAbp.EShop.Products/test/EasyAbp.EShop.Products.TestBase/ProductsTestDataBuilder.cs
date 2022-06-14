using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products
{
    public class ProductsTestDataBuilder : ITransientDependency
    {
        private readonly IProductManager _productManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IAttributeOptionIdsSerializer _attributeOptionIdsSerializer;

        public ProductsTestDataBuilder(
            IProductManager productManager,
            IUnitOfWorkManager unitOfWorkManager,
            IProductDetailRepository productDetailRepository,
            IAttributeOptionIdsSerializer attributeOptionIdsSerializer)
        {
            _productManager = productManager;
            _unitOfWorkManager = unitOfWorkManager;
            _productDetailRepository = productDetailRepository;
            _attributeOptionIdsSerializer = attributeOptionIdsSerializer;
        }

        public void Build()
        {
            AsyncHelper.RunSync(BuildAsync);
        }

        public async Task BuildAsync()
        {
            using var uow = _unitOfWorkManager.Begin();
            
            var productDetail1 = await _productDetailRepository.InsertAsync(
                new ProductDetail(ProductsTestData.ProductDetails1Id, null, ProductsTestData.Store1Id,
                    "Product details for store 1"), true);
            
            var productDetail2 = await _productDetailRepository.InsertAsync(
                new ProductDetail(ProductsTestData.ProductDetails2Id, null, ProductsTestData.Store1Id,
                    "Product details for store 1"), true);

            var product = new Product(ProductsTestData.Product1Id, null, ProductsTestData.Store1Id, "Default",
                productDetail1.Id, "Cake", "Cake", InventoryStrategy.NoNeed, null, true, false, false, null, null, 0);

            var attribute1 = new ProductAttribute(ProductsTestData.Product1Attribute1Id, "Size", null, 2);
            var attribute2 = new ProductAttribute(ProductsTestData.Product1Attribute2Id, "Color", null, 1);

            attribute1.ProductAttributeOptions.AddRange(new[]
            {
                new ProductAttributeOption(ProductsTestData.Product1Attribute1Option4Id, "XL", null, 1),
                new ProductAttributeOption(ProductsTestData.Product1Attribute1Option2Id, "M", null, 3),
                new ProductAttributeOption(ProductsTestData.Product1Attribute1Option1Id, "S", null, 4),
                new ProductAttributeOption(ProductsTestData.Product1Attribute1Option3Id, "L", null, 2),
            });
            
            attribute2.ProductAttributeOptions.AddRange(new[]
            {
                new ProductAttributeOption(ProductsTestData.Product1Attribute2Option2Id, "Green", null, 1),
                new ProductAttributeOption(ProductsTestData.Product1Attribute2Option1Id, "Red", null, 2),
            });

            product.ProductAttributes.Add(attribute2);
            product.ProductAttributes.Add(attribute1);

            await _productManager.CreateAsync(product);

            var productSku1 = new ProductSku(ProductsTestData.Product1Sku1Id,
                await _attributeOptionIdsSerializer.SerializeAsync(new[]
                    { ProductsTestData.Product1Attribute1Option1Id, ProductsTestData.Product1Attribute2Option1Id }),
                null, "USD", null, 1m, 1, 10, null, null, null);

            var productSku2 = new ProductSku(ProductsTestData.Product1Sku2Id,
                await _attributeOptionIdsSerializer.SerializeAsync(new[]
                    { ProductsTestData.Product1Attribute1Option2Id, ProductsTestData.Product1Attribute2Option1Id }),
                null, "USD", null, 2m, 1, 10, null, null, null);

            var productSku3 = new ProductSku(ProductsTestData.Product1Sku3Id,
                await _attributeOptionIdsSerializer.SerializeAsync(new[]
                    { ProductsTestData.Product1Attribute1Option3Id, ProductsTestData.Product1Attribute2Option2Id }),
                null, "USD", null, 3m, 1, 10, null, null, null);
            
            await _productManager.CreateSkuAsync(product, productSku1);
            await _productManager.CreateSkuAsync(product, productSku2);
            await _productManager.CreateSkuAsync(product, productSku3);

            await uow.CompleteAsync();
        }
    }
}
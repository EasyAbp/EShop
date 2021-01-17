using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductHistories;
using EasyAbp.EShop.Products.ProductDetailHistories;
using EasyAbp.EShop.Products.ProductInventories;

namespace EasyAbp.EShop.Products.EntityFrameworkCore
{
    [ConnectionStringName(ProductsDbProperties.ConnectionStringName)]
    public interface IProductsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Product> Products { get; set; }
        DbSet<ProductDetail> ProductDetails { get; set; }
        DbSet<ProductAttribute> ProductAttributes { get; set; }
        DbSet<ProductAttributeOption> ProductAttributeOptions { get; set; }
        DbSet<ProductSku> ProductSkus { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<ProductHistory> ProductHistories { get; set; }
        DbSet<ProductDetailHistory> ProductDetailHistories { get; set; }
        DbSet<ProductInventory> ProductInventories { get; set; }
    }
}

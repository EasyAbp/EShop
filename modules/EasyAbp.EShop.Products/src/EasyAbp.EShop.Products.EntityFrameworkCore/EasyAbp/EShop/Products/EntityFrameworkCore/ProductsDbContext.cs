using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.EntityFrameworkCore.ValueMappings;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductHistories;
using EasyAbp.EShop.Products.ProductDetailHistories;
using EasyAbp.EShop.Products.ProductInventories;

namespace EasyAbp.EShop.Products.EntityFrameworkCore
{
    [ConnectionStringName(ProductsDbProperties.ConnectionStringName)]
    public class ProductsDbContext : AbpDbContext<ProductsDbContext>, IProductsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeOption> ProductAttributeOptions { get; set; }
        public DbSet<ProductSku> ProductSkus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<ProductDetailHistory> ProductDetailHistories { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductView> ProductViews { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEShopProducts();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<List<ProductDiscountInfoModel>>()
                .HaveConversion<ProductDiscountsInfoValueConverter>();
            configurationBuilder.Properties<List<OrderDiscountPreviewInfoModel>>()
                .HaveConversion<OrderDiscountPreviewsInfoValueConverter>();
        }
    }
}

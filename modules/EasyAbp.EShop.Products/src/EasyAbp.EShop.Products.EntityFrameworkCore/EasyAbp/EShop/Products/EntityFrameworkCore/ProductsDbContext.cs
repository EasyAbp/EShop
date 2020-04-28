using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.ProductTypes;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductStores;

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
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductStore> ProductStores { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureProducts();
        }
    }
}

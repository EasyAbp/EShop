using EasyAbp.EShop.Products.ProductStores;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductTypes;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.Products;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Products.EntityFrameworkCore
{
    public static class ProductsDbContextModelCreatingExtensions
    {
        public static void ConfigureProducts(
            this ModelBuilder builder,
            Action<ProductsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ProductsModelBuilderConfigurationOptions(
                ProductsDbProperties.DbTablePrefix,
                ProductsDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<Product>(b =>
            {
                b.ToTable(options.TablePrefix + "Products", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });
            
            builder.Entity<ProductDetail>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductDetails", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
                b.HasKey(x => new { x.ProductId });
            });
            
            builder.Entity<ProductAttribute>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductAttributes", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });
            
            builder.Entity<ProductAttributeOption>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductAttributeOptions", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });
            
            builder.Entity<ProductSku>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductSkus", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
                b.Property(x => x.Price).HasColumnType("decimal(18,6)");
                b.Property(x => x.OriginalPrice).HasColumnType("decimal(18,6)");
            });

            builder.Entity<Category>(b =>
            {
                b.ToTable(options.TablePrefix + "Categories", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });

            builder.Entity<ProductType>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductTypes", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });

            builder.Entity<ProductCategory>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductCategories", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });

            builder.Entity<ProductStore>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductStores", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });
        }
    }
}

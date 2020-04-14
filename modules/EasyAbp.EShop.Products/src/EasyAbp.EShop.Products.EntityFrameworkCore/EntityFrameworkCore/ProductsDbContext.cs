﻿using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.EntityFrameworkCore
{
    [ConnectionStringName(ProductsDbProperties.ConnectionStringName)]
    public class ProductsDbContext : AbpDbContext<ProductsDbContext>, IProductsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

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
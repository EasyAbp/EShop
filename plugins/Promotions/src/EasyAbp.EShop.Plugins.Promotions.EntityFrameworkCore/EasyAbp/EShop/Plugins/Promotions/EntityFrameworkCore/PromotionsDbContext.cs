﻿using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Promotions.Promotions;

namespace EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore;

[ConnectionStringName(PromotionsDbProperties.ConnectionStringName)]
public class PromotionsDbContext : AbpDbContext<PromotionsDbContext>, IPromotionsDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<Promotion> Promotions { get; set; }

    public PromotionsDbContext(DbContextOptions<PromotionsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureEShopPluginsPromotions();
    }
}

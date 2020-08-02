using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.StoreApproval.EntityFrameworkCore
{
    public static class StoreApprovalDbContextModelCreatingExtensions
    {
        public static void ConfigureStoreApproval(
            this ModelBuilder builder,
            Action<StoreApprovalModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new StoreApprovalModelBuilderConfigurationOptions(
                StoreApprovalDbProperties.DbTablePrefix,
                StoreApprovalDbProperties.DbSchema
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


            builder.Entity<StoreApplication>(b =>
            {
                b.ToTable(options.TablePrefix + "StoreApplications", options.Schema);
                b.ConfigureByConvention();
                
                /* Configure more properties here */
            });
        }
    }
}

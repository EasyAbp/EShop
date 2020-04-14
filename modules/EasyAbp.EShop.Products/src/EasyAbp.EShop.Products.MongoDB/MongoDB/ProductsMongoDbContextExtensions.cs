using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Products.MongoDB
{
    public static class ProductsMongoDbContextExtensions
    {
        public static void ConfigureProducts(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ProductsMongoModelBuilderConfigurationOptions(
                ProductsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}
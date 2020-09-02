using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Options.ProductGroups
{
    public class ProductGroupConfigurations
    {
        private readonly Dictionary<string, ProductGroupConfiguration> _groups;
        
        public ProductGroupConfigurations()
        {
            _groups = new Dictionary<string, ProductGroupConfiguration>();
        }

        public ProductGroupConfigurations Configure<TGroup>(
            Action<ProductGroupConfiguration> configureAction)
        {
            return Configure(
                ProductGroupNameAttribute.GetGroupName<TGroup>(),
                configureAction
            );
        }

        public ProductGroupConfigurations Configure(
            [NotNull] string name,
            [NotNull] Action<ProductGroupConfiguration> configureAction)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(configureAction, nameof(configureAction));

            configureAction(
                _groups.GetOrAdd(
                    name,
                    () => new ProductGroupConfiguration()
                )
            );

            return this;
        }

        public ProductGroupConfigurations ConfigureAll(Action<string, ProductGroupConfiguration> configureAction)
        {
            foreach (var group in _groups)
            {
                configureAction(group.Key, group.Value);
            }
            
            return this;
        }

        [NotNull]
        public ProductGroupConfiguration GetConfiguration<TGroup>()
        {
            return GetConfiguration(ProductGroupNameAttribute.GetGroupName<TGroup>());
        }

        [NotNull]
        public ProductGroupConfiguration GetConfiguration([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            return _groups.GetOrDefault(name);
        }
        
        [NotNull]
        public Dictionary<string, ProductGroupConfiguration> GetConfigurationsDictionary()
        {
            return _groups;
        }
    }
}
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Options.InventoryProviders
{
    public class InventoryProviderConfigurations
    {
        private readonly Dictionary<string, InventoryProviderConfiguration> _providers;

        public InventoryProviderConfigurations()
        {
            _providers = new Dictionary<string, InventoryProviderConfiguration>();
        }

        public InventoryProviderConfigurations Configure(
            [NotNull] string name,
            [NotNull] Action<InventoryProviderConfiguration> configureAction)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(configureAction, nameof(configureAction));

            configureAction(
                _providers.GetOrAdd(
                    name,
                    () => new InventoryProviderConfiguration()
                )
            );

            return this;
        }

        public InventoryProviderConfigurations ConfigureAll(
            Action<string, InventoryProviderConfiguration> configureAction)
        {
            foreach (var provider in _providers)
            {
                configureAction(provider.Key, provider.Value);
            }

            return this;
        }

        [NotNull]
        public InventoryProviderConfiguration GetConfiguration([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            return _providers.GetOrDefault(name);
        }

        [NotNull]
        public Dictionary<string, InventoryProviderConfiguration> GetConfigurationsDictionary()
        {
            return _providers;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders
{
    public class PurchasableChecker : IPurchasableChecker, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public PurchasableChecker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual async Task CheckAsync(CreateOrderDto input, Dictionary<Guid, ProductDto> productDict,
            Dictionary<string, object> orderExtraProperties)
        {
            var providers = _serviceProvider.GetServices<IPurchasableCheckProvider>();

            foreach (var provider in providers)
            {
                await provider.CheckAsync(input, productDict, orderExtraProperties);
            }
        }
    }
}
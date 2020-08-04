using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.PaymentService.Payments;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PayableChecker : IPayableChecker, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public PayableChecker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public virtual async Task CheckAsync(CreatePaymentDto input, List<OrderDto> orders, CreatePaymentEto createPaymentEto)
        {
            var providers = _serviceProvider.GetServices<IPayableCheckProvider>();

            foreach (var provider in providers)
            {
                await provider.CheckAsync(input, orders, createPaymentEto);
            }
        }
    }
}
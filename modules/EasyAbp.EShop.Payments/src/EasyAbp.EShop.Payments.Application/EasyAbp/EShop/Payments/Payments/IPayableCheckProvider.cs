﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.PaymentService.Payments;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPayableCheckProvider
    {
        Task CheckAsync(CreatePaymentDto input, List<OrderDto> orders, CreatePaymentEto createPaymentEto);
    }
}
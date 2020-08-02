﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderManager : IDomainService
    {
        Task<Order> DiscountAsync(Order order);

        Task<Order> CompleteAsync(Order order);
    }
}
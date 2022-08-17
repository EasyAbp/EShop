using System;
using EasyAbp.EShop.Stores.Stores;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public interface IFlashSaleResult : IMultiStore
{
    Guid PlanId { get; }

    FlashSaleResultStatus Status { get; }

    string Reason { get; }

    Guid UserId { get; }

    Guid? OrderId { get; }
}
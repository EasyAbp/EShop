using System;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems;

public interface IServerSideBasketItemInfo : IBasketItemInfo
{
    Guid UserId { get; }
}
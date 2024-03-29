﻿using EasyAbp.EShop.Products.Products;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class UnexpectedInventoryStrategyException : BusinessException
{
    public UnexpectedInventoryStrategyException(InventoryStrategy expectedInventoryStrategy) : base(FlashSalesErrorCodes.UnexpectedInventoryStrategy)
    {
        WithData(nameof(expectedInventoryStrategy), expectedInventoryStrategy);
    }
}

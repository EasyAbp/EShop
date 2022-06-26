using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public interface IFlashSalesPlanRepository : IRepository<FlashSalesPlan, Guid>
{
}
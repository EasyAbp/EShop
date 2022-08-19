using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public interface IFlashSalePlanRepository : IRepository<FlashSalePlan, Guid>
{
}
using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public interface IFlashSalesResultRepository : IRepository<FlashSalesResult, Guid>
{

}

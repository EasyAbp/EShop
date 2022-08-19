using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public interface IFlashSaleResultRepository : IRepository<FlashSaleResult, Guid>
{

}

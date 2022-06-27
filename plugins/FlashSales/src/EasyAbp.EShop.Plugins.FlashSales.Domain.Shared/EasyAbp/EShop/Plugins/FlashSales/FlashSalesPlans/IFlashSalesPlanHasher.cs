using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public interface IFlashSalesPlanHasher
{
    Task<string> HashAsync(DateTime? planLastModificationTime, DateTime? productLastModificationTime, DateTime? productSkuLastModificationTime);
}
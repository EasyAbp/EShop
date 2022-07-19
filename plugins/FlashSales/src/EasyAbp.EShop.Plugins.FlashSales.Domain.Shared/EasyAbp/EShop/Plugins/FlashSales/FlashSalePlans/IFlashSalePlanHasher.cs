using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public interface IFlashSalePlanHasher
{
    Task<string> HashAsync(DateTime? planLastModificationTime, DateTime? productLastModificationTime, DateTime? productSkuLastModificationTime);
}
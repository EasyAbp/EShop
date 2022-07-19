using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSalePlanHasher : IFlashSalePlanHasher, ITransientDependency
{
    public virtual Task<string> HashAsync(DateTime? planLastModificationTime, DateTime? productLastModificationTime, DateTime? productSkuLastModificationTime)
    {
        var input = $"{planLastModificationTime?.Ticks}|{productLastModificationTime?.Ticks}|{productSkuLastModificationTime?.Ticks}";

        return Task.FromResult(CreateMd5(input));
    }

    private static string CreateMd5(string input)
    {
        using var md5 = MD5.Create();

        var inputBytes = Encoding.UTF8.GetBytes(input);

        var sb = new StringBuilder();
        foreach (var t in md5.ComputeHash(inputBytes))
        {
            sb.Append(t.ToString("X2"));
        }
        return sb.ToString();
    }
}

using System;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders;

public static class HasExtraPropertiesExtensions
{
    public static DateTime? FindDateTimeProperty(this IHasExtraProperties extraProperties, string name)
    {
        var objValue = extraProperties.GetProperty(name);

        return objValue switch
        {
            null => null,
            DateTime span => span,
            _ => DateTime.Parse((string)objValue)
        };
    }

    public static TimeSpan? FindTimeSpanProperty(this IHasExtraProperties extraProperties, string name)
    {
        var objValue = extraProperties.GetProperty(name);

        return objValue switch
        {
            null => null,
            TimeSpan span => span,
            _ => TimeSpan.Parse((string)objValue)
        };
    }
}
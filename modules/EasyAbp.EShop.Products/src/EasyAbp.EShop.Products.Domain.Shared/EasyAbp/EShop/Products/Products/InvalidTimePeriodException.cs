using Volo.Abp;

namespace EasyAbp.EShop.Products.Products;

public class InvalidTimePeriodException : BusinessException
{
    public InvalidTimePeriodException() : base(ProductsErrorCodes.InvalidTimePeriod)
    {
    }
}
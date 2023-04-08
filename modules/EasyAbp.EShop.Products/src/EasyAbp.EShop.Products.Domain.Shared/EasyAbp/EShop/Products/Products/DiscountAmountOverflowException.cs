using Volo.Abp;

namespace EasyAbp.EShop.Products.Products;

public class DiscountAmountOverflowException : BusinessException
{
    public DiscountAmountOverflowException() : base(ProductsErrorCodes.DiscountAmountOverflow)
    {
    }
}
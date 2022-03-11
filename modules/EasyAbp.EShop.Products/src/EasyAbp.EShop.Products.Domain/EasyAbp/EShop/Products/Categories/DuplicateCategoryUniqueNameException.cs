using Volo.Abp;

namespace EasyAbp.EShop.Products.Categories
{
    public class DuplicateCategoryUniqueNameException : BusinessException
    {
        public DuplicateCategoryUniqueNameException(string uniqueName) : base(ProductsErrorCodes.DuplicateCategoryUniqueName)
        {
            WithData(nameof(uniqueName), uniqueName);
        }
    }
}
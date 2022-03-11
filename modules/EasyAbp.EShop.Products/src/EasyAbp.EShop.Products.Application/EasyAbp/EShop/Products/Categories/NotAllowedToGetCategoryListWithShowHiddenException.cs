using Volo.Abp;

namespace EasyAbp.EShop.Products.Categories
{
    public class NotAllowedToGetCategoryListWithShowHiddenException : BusinessException
    {
        public NotAllowedToGetCategoryListWithShowHiddenException() : base(ProductsErrorCodes.NotAllowedToGetCategoryListWithShowHidden)
        {
        }
    }
}
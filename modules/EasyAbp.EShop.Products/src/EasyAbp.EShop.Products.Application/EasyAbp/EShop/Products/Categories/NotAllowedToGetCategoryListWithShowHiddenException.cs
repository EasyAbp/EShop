using Volo.Abp;

namespace EasyAbp.EShop.Products.Categories
{
    public class NotAllowedToGetCategoryListWithShowHiddenException : BusinessException
    {
        public NotAllowedToGetCategoryListWithShowHiddenException() : base(
            message: $"You have no permission to get category list with hidden categories.")
        {
        }
    }
}
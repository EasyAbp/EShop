using Volo.Abp;

namespace EasyAbp.EShop.Products.Categories
{
    public class DuplicateCategoryUniqueNameException : BusinessException
    {
        public DuplicateCategoryUniqueNameException() : base("DuplicateCategoryUniqueName")
        {
            
        }
    }
}
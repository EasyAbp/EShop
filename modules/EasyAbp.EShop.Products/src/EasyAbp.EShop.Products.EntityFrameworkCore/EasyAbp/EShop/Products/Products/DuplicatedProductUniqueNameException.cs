using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class DuplicatedProductUniqueNameException : BusinessException
    {
        public DuplicatedProductUniqueNameException(string uniqueName) : base("DuplicatedProductUniqueName",
            $"The product unique name \"{uniqueName}\" is duplicated.")
        {

        }
    }
}
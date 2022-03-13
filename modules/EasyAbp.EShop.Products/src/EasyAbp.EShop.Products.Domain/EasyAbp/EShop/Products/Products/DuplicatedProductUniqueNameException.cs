using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class DuplicatedProductUniqueNameException : BusinessException
    {
        public DuplicatedProductUniqueNameException(string uniqueName) 
            : base(ProductsErrorCodes.DuplicatedProductUniqueName)
        {
            WithData(nameof(uniqueName), uniqueName);
        }
    }
}
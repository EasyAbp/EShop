using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Products.Authorization
{
    public class ProductsPermissions
    {
        public const string GroupName = "Products";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ProductsPermissions));
        }
    }
}
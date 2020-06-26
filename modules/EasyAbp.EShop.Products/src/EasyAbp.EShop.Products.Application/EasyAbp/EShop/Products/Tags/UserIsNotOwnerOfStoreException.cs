using Volo.Abp;

namespace EasyAbp.EShop.Products.Tags
{
    public class UserIsNotOwnerOfStoreException : BusinessException
    {
        public UserIsNotOwnerOfStoreException() : base(
            message: $"You have to be owner of store to modify tags.")
        {
        }
    }
}

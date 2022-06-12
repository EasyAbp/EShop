using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class NonexistentInventoryProviderException : BusinessException
    {
        public NonexistentInventoryProviderException(string inventoryProviderName) :
            base(ProductsErrorCodes.NonexistentInventoryProvider)
        {
            WithData(nameof(inventoryProviderName), inventoryProviderName);
        }
    }
}
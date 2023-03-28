using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProduct : IProductBase
    {
        IEnumerable<IProductAttribute> ProductAttributes { get; }

        IEnumerable<IProductSku> ProductSkus { get; }
    }
}
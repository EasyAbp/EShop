using EasyAbp.EShop.Products.Products;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Products
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductToProductEtoMapper : MapperBase<Product, ProductEto>
    {
        public override partial ProductEto Map(Product source);

        public override partial void Map(Product source, ProductEto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAttributeToProductAttributeEtoMapper : MapperBase<ProductAttribute, ProductAttributeEto>
    {
        public override partial ProductAttributeEto Map(ProductAttribute source);

        public override partial void Map(ProductAttribute source, ProductAttributeEto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductAttributeOptionToProductAttributeOptionEtoMapper : MapperBase<ProductAttributeOption, ProductAttributeOptionEto>
    {
        public override partial ProductAttributeOptionEto Map(ProductAttributeOption source);

        public override partial void Map(ProductAttributeOption source, ProductAttributeOptionEto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ProductSkuToProductSkuEtoMapper : MapperBase<ProductSku, ProductSkuEto>
    {
        public override partial ProductSkuEto Map(ProductSku source);

        public override partial void Map(ProductSku source, ProductSkuEto destination);
    }
}

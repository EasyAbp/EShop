using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.ProductDetails;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>, IProduct
    {
        public virtual Guid ProductTypeId { get; protected set; }
        
        public virtual Guid ProductDetailId { get; protected set; }

        [CanBeNull]
        public virtual string UniqueName { get; protected set; }

        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        public virtual InventoryStrategy InventoryStrategy { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; }

        public virtual int DisplayOrder { get; protected set; }

        public virtual bool IsPublished { get; protected set; }
        
        public virtual bool IsStatic { get; protected set; }
        
        public virtual bool IsHidden { get; protected set; }
        
        public virtual List<ProductAttribute> ProductAttributes { get; protected set; }
        
        public virtual List<ProductSku> ProductSkus { get; protected set; }

        protected Product()
        {
        }

        public Product(
            Guid id,
            Guid productTypeId,
            Guid productDetailId,
            [CanBeNull] string code,
            [NotNull] string displayName,
            InventoryStrategy inventoryStrategy,
            bool isPublished,
            bool isStatic,
            bool isHidden,
            [CanBeNull] string mediaResources,
            int displayOrder
        ) : base(id)
        {
            ProductTypeId = productTypeId;
            ProductDetailId = productDetailId;
            UniqueName = code?.Trim();
            DisplayName = displayName;
            InventoryStrategy = inventoryStrategy;
            IsPublished = isPublished;
            IsStatic = isStatic;
            IsHidden = isHidden;
            MediaResources = mediaResources;
            DisplayOrder = displayOrder;
            
            ProductAttributes = new List<ProductAttribute>();
            ProductSkus = new List<ProductSku>();
        }

        public void InitializeNullCollections()
        {
            ProductAttributes ??= new List<ProductAttribute>();
            ProductSkus ??= new List<ProductSku>();
        }

        public void TrimCode()
        {
            UniqueName = UniqueName?.Trim();
        }
    }
}

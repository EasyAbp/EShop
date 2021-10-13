using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>, IProduct, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid StoreId { get; protected set; }

        [NotNull]
        public virtual string ProductGroupName { get; protected set; }
        
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
        
        public virtual TimeSpan? PaymentExpireIn { get; protected set; }
        
        public virtual List<ProductAttribute> ProductAttributes { get; protected set; }
        
        public virtual List<ProductSku> ProductSkus { get; protected set; }

        protected Product()
        {
        }

        public Product(
            Guid id,
            Guid? tenantId,
            Guid storeId,
            [NotNull] string productGroupName,
            Guid productDetailId,
            [CanBeNull] string uniqueName,
            [NotNull] string displayName,
            InventoryStrategy inventoryStrategy,
            bool isPublished,
            bool isStatic,
            bool isHidden,
            TimeSpan? paymentExpireIn,
            [CanBeNull] string mediaResources,
            int displayOrder
        ) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            ProductGroupName = productGroupName;
            ProductDetailId = productDetailId;
            UniqueName = uniqueName?.Trim();
            DisplayName = displayName;
            InventoryStrategy = inventoryStrategy;
            IsPublished = isPublished;
            IsStatic = isStatic;
            IsHidden = isHidden;
            PaymentExpireIn = paymentExpireIn;
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

        public void TrimUniqueName()
        {
            UniqueName = UniqueName?.Trim();
        }
    }
}

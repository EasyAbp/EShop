using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductView : CreationAuditedAggregateRoot<Guid>, IProduct
    {
        #region Properties of IProduct

        public virtual Guid StoreId { get; protected set; }

        public virtual string ProductGroupName { get; protected set; }

        public virtual Guid ProductDetailId { get; protected set; }

        public virtual string UniqueName { get; protected set; }

        public virtual string DisplayName { get; protected set; }

        public virtual InventoryStrategy InventoryStrategy { get; protected set; }

        public virtual string MediaResources { get; protected set; }

        public virtual int DisplayOrder { get; protected set; }

        public virtual bool IsPublished { get; protected set; }

        public virtual bool IsStatic { get; protected set; }

        public virtual bool IsHidden { get; protected set; }

        #endregion
        
        public virtual string ProductGroupDisplayName { get; protected set; }
        
        public virtual decimal? MinimumPrice { get; protected set; }
        
        public virtual decimal? MaximumPrice { get; protected set; }
        
        public virtual long Sold { get; protected set; }
        
        protected ProductView()
        {
        }
        
        public ProductView(
            Guid id,
            Guid storeId,
            string productGroupName,
            Guid productDetailId,
            string uniqueName,
            string displayName,
            InventoryStrategy inventoryStrategy,
            bool isPublished,
            bool isStatic,
            bool isHidden,
            string mediaResources,
            int displayOrder,
            string productGroupDisplayName,
            decimal? minimumPrice,
            decimal? maximumPrice,
            long sold
        ) : base(id)
        {
            StoreId = storeId;
            ProductGroupName = productGroupName;
            ProductDetailId = productDetailId;
            UniqueName = uniqueName?.Trim();
            DisplayName = displayName;
            InventoryStrategy = inventoryStrategy;
            IsPublished = isPublished;
            IsStatic = isStatic;
            IsHidden = isHidden;
            MediaResources = mediaResources;
            DisplayOrder = displayOrder;
            
            ProductGroupDisplayName = productGroupDisplayName;
            MinimumPrice = minimumPrice;
            MaximumPrice = maximumPrice;
            Sold = sold;
        }
        
        public void SetSold(long sold)
        {
            Sold = sold;
        }
    }
}

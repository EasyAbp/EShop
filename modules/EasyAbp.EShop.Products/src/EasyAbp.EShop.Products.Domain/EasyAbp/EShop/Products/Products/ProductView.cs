using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductView : CreationAuditedAggregateRoot<Guid>, IProductView, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        #region Properties of IProductBase

        public virtual Guid StoreId { get; protected set; }

        public virtual string ProductGroupName { get; protected set; }

        public virtual Guid? ProductDetailId { get; protected set; }

        public virtual string UniqueName { get; protected set; }

        public virtual string DisplayName { get; protected set; }

        public virtual string Overview { get; protected set; }

        public virtual InventoryStrategy InventoryStrategy { get; protected set; }

        public string InventoryProviderName { get; protected set; }

        public virtual string MediaResources { get; protected set; }

        public virtual int DisplayOrder { get; protected set; }

        public virtual bool IsPublished { get; protected set; }

        public virtual bool IsStatic { get; protected set; }

        public virtual bool IsHidden { get; protected set; }

        public virtual TimeSpan? PaymentExpireIn { get; protected set; }

        #endregion

        public virtual string ProductGroupDisplayName { get; protected set; }

        public virtual List<ProductDiscountInfoModel> ProductDiscounts { get; set; }

        public virtual List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; set; }

        public virtual decimal? MinimumPrice { get; protected set; }

        public virtual decimal? MaximumPrice { get; protected set; }

        public virtual decimal? MinimumPriceWithoutDiscount { get; protected set; }

        public virtual decimal? MaximumPriceWithoutDiscount { get; protected set; }

        public virtual long Sold { get; protected set; }

        protected ProductView()
        {
        }

        public ProductView(
            Guid id,
            Guid? tenantId,
            Guid storeId,
            string productGroupName,
            Guid? productDetailId,
            string uniqueName,
            string displayName,
            string overview,
            InventoryStrategy inventoryStrategy,
            string inventoryProviderName,
            bool isPublished,
            bool isStatic,
            bool isHidden,
            TimeSpan? paymentExpireIn,
            string mediaResources,
            int displayOrder,
            string productGroupDisplayName,
            List<ProductDiscountInfoModel> productDiscounts,
            List<OrderDiscountPreviewInfoModel> orderDiscountPreviews,
            decimal? minimumPrice,
            decimal? maximumPrice,
            decimal? minimumPriceWithoutDiscount,
            decimal? maximumPriceWithoutDiscount,
            long sold
        ) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            ProductGroupName = productGroupName;
            ProductDetailId = productDetailId;
            UniqueName = uniqueName?.Trim();
            DisplayName = displayName;
            Overview = overview;
            InventoryStrategy = inventoryStrategy;
            InventoryProviderName = inventoryProviderName;
            IsPublished = isPublished;
            IsStatic = isStatic;
            IsHidden = isHidden;
            PaymentExpireIn = paymentExpireIn;
            MediaResources = mediaResources;
            DisplayOrder = displayOrder;

            ProductGroupDisplayName = productGroupDisplayName;
            ProductDiscounts = productDiscounts ?? new List<ProductDiscountInfoModel>();
            OrderDiscountPreviews = orderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
            MinimumPrice = minimumPrice;
            MaximumPrice = maximumPrice;
            MinimumPriceWithoutDiscount = minimumPriceWithoutDiscount;
            MaximumPriceWithoutDiscount = maximumPriceWithoutDiscount;
            Sold = sold;
        }

        public void SetSold(long sold)
        {
            Sold = sold;
        }

        public void SetPrices(decimal? min, decimal? max, decimal? minWithoutDiscount, decimal? maxWithoutDiscount)
        {
            MinimumPrice = min;
            MaximumPrice = max;
            MinimumPriceWithoutDiscount = minWithoutDiscount;
            MaximumPriceWithoutDiscount = maxWithoutDiscount;
        }

        public void SetDiscounts(IHasDiscountsForProduct discountsForProduct)
        {
            ProductDiscounts = discountsForProduct.ProductDiscounts ?? new List<ProductDiscountInfoModel>();
            OrderDiscountPreviews = discountsForProduct.OrderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
        }
    }
}
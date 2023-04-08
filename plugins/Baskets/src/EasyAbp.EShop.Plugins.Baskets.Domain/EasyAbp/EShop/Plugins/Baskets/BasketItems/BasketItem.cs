using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class BasketItem : AuditedAggregateRoot<Guid>, IBasketItem, IServerSideBasketItemInfo, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual string BasketName { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        public virtual Guid ProductId { get; protected set; }

        public virtual Guid ProductSkuId { get; protected set; }

        public virtual int Quantity { get; protected set; }

        public virtual decimal PriceWithoutDiscount { get; protected set; }

        public virtual decimal TotalPriceWithoutDiscount { get; protected set; }

        [CanBeNull]
        public virtual string MediaResources { get; protected set; }

        [CanBeNull]
        public virtual string ProductUniqueName { get; protected set; }

        [NotNull]
        public virtual string ProductDisplayName { get; protected set; }

        [CanBeNull]
        public virtual string SkuName { get; protected set; }

        [NotNull]
        public virtual string SkuDescription { get; protected set; }

        [NotNull]
        public virtual string Currency { get; protected set; }

        public virtual int Inventory { get; protected set; }

        public virtual bool IsInvalid { get; protected set; }

        public virtual List<ProductDiscountInfoModel> ProductDiscounts { get; protected set; }

        public virtual List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; protected set; }

        protected BasketItem()
        {
        }

        public BasketItem(
            Guid id,
            Guid? tenantId,
            [NotNull] string basketName,
            Guid userId,
            Guid storeId,
            Guid productId,
            Guid productSkuId,
            List<ProductDiscountInfoModel> productDiscounts,
            List<OrderDiscountPreviewInfoModel> orderDiscountPreviews) : base(id)
        {
            TenantId = tenantId;
            BasketName = basketName;
            UserId = userId;
            StoreId = storeId;
            ProductId = productId;
            ProductSkuId = productSkuId;
            ProductDiscounts = productDiscounts ?? new List<ProductDiscountInfoModel>();
            OrderDiscountPreviews = orderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
        }

        public void Update(int quantity, IProductData productData)
        {
            Quantity = quantity;

            MediaResources = productData.MediaResources;
            ProductUniqueName = productData.ProductUniqueName;
            ProductDisplayName = productData.ProductDisplayName;
            SkuName = productData.SkuName;
            SkuDescription = productData.SkuDescription;
            Currency = productData.Currency;
            PriceWithoutDiscount = productData.PriceWithoutDiscount;
            TotalPriceWithoutDiscount = productData.PriceWithoutDiscount * quantity;
            Inventory = productData.Inventory;
        }

        public void SetIsInvalid(bool isInvalid)
        {
            IsInvalid = isInvalid;
        }
    }
}
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

        public virtual List<ProductDiscountInfoModel> ProductDiscounts { get; protected set; }

        public virtual List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; protected set; }

        public virtual decimal? MinimumPrice { get; protected set; }

        public virtual decimal? MaximumPrice { get; protected set; }

        public virtual decimal? MinimumPriceWithoutDiscount { get; protected set; }

        public virtual decimal? MaximumPriceWithoutDiscount { get; protected set; }

        public virtual long Sold { get; protected set; }

        protected ProductView()
        {
        }

        public ProductView(Product product, string productGroupDisplayName) : base(product.Id)
        {
            TenantId = product.TenantId;
            StoreId = product.StoreId;
            ProductGroupName = product.ProductGroupName;
            ProductDetailId = product.ProductDetailId;
            UniqueName = product.UniqueName?.Trim();
            DisplayName = product.DisplayName;
            Overview = product.Overview;
            InventoryStrategy = product.InventoryStrategy;
            InventoryProviderName = product.InventoryProviderName;
            IsPublished = product.IsPublished;
            IsStatic = product.IsStatic;
            IsHidden = product.IsHidden;
            PaymentExpireIn = product.PaymentExpireIn;
            MediaResources = product.MediaResources;
            DisplayOrder = product.DisplayOrder;

            ProductGroupDisplayName = productGroupDisplayName;

            ProductDiscounts = new List<ProductDiscountInfoModel>();
            OrderDiscountPreviews = new List<OrderDiscountPreviewInfoModel>();
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

        public void SetDiscounts(IHasDiscountsForSku discountsForSku)
        {
            ProductDiscounts = discountsForSku.ProductDiscounts ?? new List<ProductDiscountInfoModel>();
            OrderDiscountPreviews = discountsForSku.OrderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
        }
    }
}
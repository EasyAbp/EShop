using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class BasketItemDto : ExtensibleAuditedEntityDto<Guid>, IServerSideBasketItemInfo
    {
        public string BasketName { get; set; }

        public Guid UserId { get; set; }

        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutDiscount { get; set; }

        public decimal TotalPriceWithoutDiscount { get; set; }

        public string MediaResources { get; set; }

        public string ProductUniqueName { get; set; }

        public string ProductDisplayName { get; set; }

        public string SkuName { get; set; }

        public string SkuDescription { get; set; }

        public string Currency { get; set; }

        public int Inventory { get; set; }

        public bool IsInvalid { get; set; }

        public List<ProductDiscountInfoModel> ProductDiscounts { get; set; }

        public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; set; }
    }
}
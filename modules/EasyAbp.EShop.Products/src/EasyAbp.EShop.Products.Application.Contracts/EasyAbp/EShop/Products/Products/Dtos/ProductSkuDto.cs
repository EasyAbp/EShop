using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class ProductSkuDto : ExtensibleFullAuditedEntityDto<Guid>, IProductSku, IHasFullDiscountsInfo
    {
        public List<Guid> AttributeOptionIds { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public decimal? OriginalPrice { get; set; }

        public decimal PriceWithoutDiscount { get; set; }

        public decimal Price { get; set; }

        public List<ProductDiscountInfoModel> ProductDiscounts { get; set; } = new();

        public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; set; } = new();

        public int Inventory { get; set; }

        public long Sold { get; set; }

        public int OrderMinQuantity { get; set; }

        public int OrderMaxQuantity { get; set; }

        public TimeSpan? PaymentExpireIn { get; set; }

        public string MediaResources { get; set; }

        public Guid? ProductDetailId { get; set; }
    }
}
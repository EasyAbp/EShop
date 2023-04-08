using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using JetBrains.Annotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;

[Serializable]
public class ClientSideBasketItemModel : ExtensibleObject, IBasketItem
{
    public Guid Id { get; set; }

    public string BasketName { get; set; }

    public int Quantity { get; set; }

    public Guid StoreId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductUniqueName { get; set; }

    public string ProductDisplayName { get; set; }

    public Guid ProductSkuId { get; set; }

    public string SkuName { get; set; }

    public string SkuDescription { get; set; }

    public string MediaResources { get; set; }

    public string Currency { get; set; }

    public decimal PriceWithoutDiscount { get; set; }

    public decimal TotalPriceWithoutDiscount { get; set; }

    public List<ProductDiscountInfoModel> ProductDiscounts { get; set; }

    public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; set; }

    public int Inventory { get; set; }

    public bool IsInvalid { get; set; }

    public ClientSideBasketItemModel()
    {
    }

    public ClientSideBasketItemModel(
        Guid id,
        [NotNull] string basketName,
        Guid storeId,
        Guid productId,
        Guid productSkuId,
        List<ProductDiscountInfoModel> productDiscounts,
        List<OrderDiscountPreviewInfoModel> orderDiscountPreviews)
    {
        Id = id;
        BasketName = basketName;
        StoreId = storeId;
        ProductId = productId;
        ProductSkuId = productSkuId;
        ProductDiscounts = productDiscounts ?? new List<ProductDiscountInfoModel>();
        OrderDiscountPreviews = orderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
    }

    public void SetIsInvalid(bool isInvalid)
    {
        IsInvalid = isInvalid;
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
}
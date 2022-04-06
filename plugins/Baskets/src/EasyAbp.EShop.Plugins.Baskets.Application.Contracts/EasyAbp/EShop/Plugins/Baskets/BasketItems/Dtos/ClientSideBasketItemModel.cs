using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;

[Serializable]
public class ClientSideBasketItemModel : IBasketItem
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
    
    public decimal UnitPrice { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public decimal TotalDiscount { get; set; }
    
    public int Inventory { get; set; }
    
    public bool IsInvalid { get; set; }

    public ClientSideBasketItemModel(
        Guid id,
        [NotNull] string basketName,
        Guid storeId,
        Guid productId,
        Guid productSkuId)
    {
        Id = id;
        BasketName = basketName;
        StoreId = storeId;
        ProductId = productId;
        ProductSkuId = productSkuId;
    }
    
    public void SetIsInvalid(bool isInvalid)
    {
        IsInvalid = isInvalid;
    }

    public void UpdateProductData(int quantity, IProductData productData)
    {
        Quantity = quantity;
            
        MediaResources = productData.MediaResources;
        ProductUniqueName = productData.ProductUniqueName;
        ProductDisplayName = productData.ProductDisplayName;
        SkuName = productData.SkuName;
        SkuDescription = productData.SkuDescription;
        Currency = productData.Currency;
        UnitPrice = productData.UnitPrice;
        TotalPrice = productData.TotalPrice;
        TotalDiscount = productData.TotalDiscount;
        Inventory = productData.Inventory;
    }
}
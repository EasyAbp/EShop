using System;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Orders.Orders;

[Serializable]
public class CreateOrderLineInfoModel : ExtensibleObject, ICreateOrderLineInfo
{
    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public int Quantity { get; set; }

    public CreateOrderLineInfoModel()
    {
    }

    public CreateOrderLineInfoModel(Guid productId, Guid productSkuId, int quantity)
    {
        ProductId = productId;
        ProductSkuId = productSkuId;
        Quantity = quantity;
    }
}
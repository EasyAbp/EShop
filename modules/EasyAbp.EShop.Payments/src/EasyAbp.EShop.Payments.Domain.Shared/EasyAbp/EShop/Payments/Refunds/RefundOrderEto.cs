using System;
using JetBrains.Annotations;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.Refunds;

[Serializable]
public class RefundOrderEto : CreateEShopRefundItemInfoModel, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid StoreId { get; set; }

    public Guid PaymentId { get; set; }

    [CanBeNull]
    public string DisplayReason { get; set; }

    protected RefundOrderEto()
    {
    }

    public RefundOrderEto(Guid? tenantId, Guid orderId, Guid storeId, Guid paymentId, [CanBeNull] string displayReason,
        [CanBeNull] string customerRemark, [CanBeNull] string staffRemark)
    {
        TenantId = tenantId;
        OrderId = orderId;
        StoreId = storeId;
        PaymentId = paymentId;
        DisplayReason = displayReason;
        CustomerRemark = customerRemark;
        StaffRemark = staffRemark;
    }
}
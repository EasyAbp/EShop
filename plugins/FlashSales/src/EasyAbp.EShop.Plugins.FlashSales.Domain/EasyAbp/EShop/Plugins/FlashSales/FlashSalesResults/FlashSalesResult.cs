using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public class FlashSalesResult : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual Guid StoreId { get; protected set; }

    public virtual Guid PlanId { get; protected set; }

    public virtual FlashSalesResultStatus Status { get; protected set; }

    public virtual string Reason { get; protected set; }

    public virtual Guid UserId { get; protected set; }

    public virtual Guid? OrderId { get; protected set; }

    protected FlashSalesResult() { }

    public FlashSalesResult(Guid id, Guid? tenantId, Guid storeId, Guid planId, FlashSalesResultStatus status, string reason, Guid userId, Guid? orderId)
        : base(id)
    {
        TenantId = tenantId;
        StoreId = storeId;
        PlanId = planId;
        Status = status;
        Reason = reason;
        UserId = userId;
        OrderId = orderId;
    }

    public void MarkAsSuccessful(Guid orderId)
    {
        Status = FlashSalesResultStatus.Successful;
        OrderId = orderId;
    }

    public void MarkAsFailed(string reason)
    {
        Status = FlashSalesResultStatus.Failed;
        Reason = Check.NotNullOrEmpty(reason, nameof(reason));
    }
}

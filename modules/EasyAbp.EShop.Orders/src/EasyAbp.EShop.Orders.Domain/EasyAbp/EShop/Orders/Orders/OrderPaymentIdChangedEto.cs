using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderPaymentIdChangedEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        public Order Order { get; set; }
        
        public Guid? FromPaymentId { get; set; }
        
        public Guid? ToPaymentId { get; set; }

        public OrderPaymentIdChangedEto(Guid? tenantId, Order order, Guid? fromPaymentId, Guid? toPaymentId)
        {
            TenantId = tenantId;
            Order = order;
            FromPaymentId = fromPaymentId;
            ToPaymentId = toPaymentId;
        }
    }
}
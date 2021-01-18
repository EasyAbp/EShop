using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderPaidEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public OrderEto Order { get; set; }
        
        public Guid PaymentId { get; set; }
        
        public Guid PaymentItemId { get; set; }

        public OrderPaidEto(OrderEto order, Guid paymentId, Guid paymentItemId)
        {
            TenantId = order.TenantId;
            Order = order;
            PaymentId = paymentId;
            PaymentItemId = paymentItemId;
        }
    }
}
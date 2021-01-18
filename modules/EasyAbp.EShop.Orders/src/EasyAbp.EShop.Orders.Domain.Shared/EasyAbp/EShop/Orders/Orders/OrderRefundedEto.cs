using System;
using EasyAbp.EShop.Payments.Refunds;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderRefundedEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        public OrderEto Order { get; set; }
        
        public EShopRefundEto Refund { get; set; }

        public OrderRefundedEto(OrderEto order, EShopRefundEto refund)
        {
            TenantId = order.TenantId;
            Order = order;
            Refund = refund;
        }
    }
}
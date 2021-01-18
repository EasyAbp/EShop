using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderCanceledEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        public OrderEto Order { get; set; }

        public OrderCanceledEto(OrderEto order)
        {
            TenantId = order.TenantId;
            Order = order;
        }
    }
}
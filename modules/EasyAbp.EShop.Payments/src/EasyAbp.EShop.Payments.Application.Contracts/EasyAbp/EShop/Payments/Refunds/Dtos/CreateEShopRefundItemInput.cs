using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class CreateEShopRefundItemInput
    {
        public Guid OrderId { get; set; }
        
        [CanBeNull]
        public string CustomerRemark { get; set; }
        
        [CanBeNull]
        public string StaffRemark { get; set; }
        
        public List<OrderLineRefundInfoModel> OrderLines { get; set; } = new List<OrderLineRefundInfoModel>();
    }
}
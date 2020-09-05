using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Serializable]
    public class EShopRefundItemEto : IRefundItem
    {
        #region Base properties

        public Guid Id { get; set; }

        public Guid PaymentItemId { get; set; }
        
        public decimal RefundAmount { get; set; }
        
        public string CustomerRemark { get; set; }
        
        public string StaffRemark { get; set; }
        
        public Dictionary<string, object> ExtraProperties { get; set; }
        
        #endregion
        
        public Guid StoreId { get; set; }
        
        public Guid OrderId { get; set; }
        
        public List<RefundItemOrderLineEto> RefundItemOrderLines { get; set; } = new List<RefundItemOrderLineEto>();
    }
}
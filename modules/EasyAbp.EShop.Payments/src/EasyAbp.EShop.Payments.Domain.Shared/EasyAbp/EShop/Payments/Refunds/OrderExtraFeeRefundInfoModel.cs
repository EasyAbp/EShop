using System;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Serializable]
    public class OrderExtraFeeRefundInfoModel
    {
        public string Name { get; set; }
        
        public string Key { get; set; }
        
        public decimal TotalAmount { get; set; }
    }
}
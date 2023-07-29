using System;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderExtraFeeEto : IOrderExtraFee
    {
        public Guid OrderId { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public string DisplayName { get; set; }

        public decimal Fee { get; set; }

        public decimal RefundAmount { get; set; }

        public decimal? PaymentAmount { get; set; }
    }
}
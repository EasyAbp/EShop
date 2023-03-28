using System;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    public class OrderExtraFeeDto : IOrderExtraFee
    {
        public Guid OrderId { get; }

        public string Name { get; set; }

        public string Key { get; set; }

        public decimal Fee { get; set; }

        public decimal RefundAmount { get; }
    }
}
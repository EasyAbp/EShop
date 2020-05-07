using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class CreateRefundDto
    {
        [DisplayName("RefundStoreId")]
        public Guid StoreId { get; set; }

        [DisplayName("RefundOrderId")]
        public Guid OrderId { get; set; }

        [DisplayName("RefundRefundPaymentMethod")]
        public string RefundPaymentMethod { get; set; }

        [DisplayName("RefundExternalTradingCode")]
        public string ExternalTradingCode { get; set; }

        [DisplayName("RefundCurrency")]
        public string Currency { get; set; }

        [DisplayName("RefundRefundAmount")]
        public decimal RefundAmount { get; set; }

        [DisplayName("RefundCustomerRemark")]
        public string CustomerRemark { get; set; }

        [DisplayName("RefundStaffRemark")]
        public string StaffRemark { get; set; }
    }
}
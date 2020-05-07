using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class RefundDto : FullAuditedEntityDto<Guid>
    {
        public Guid StoreId { get; set; }

        public Guid OrderId { get; set; }

        public string RefundPaymentMethod { get; set; }

        public string ExternalTradingCode { get; set; }

        public string Currency { get; set; }

        public decimal RefundAmount { get; set; }

        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }
    }
}
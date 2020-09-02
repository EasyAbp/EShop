using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class RefundDto : ExtensibleFullAuditedEntityDto<Guid>, IRefund
    {
        #region Base properties

        public Guid PaymentId { get; set; }

        public string RefundPaymentMethod { get; set; }

        public string ExternalTradingCode { get; set; }

        public string Currency { get; set; }

        public decimal RefundAmount { get; set; }
        
        public string DisplayReason { get; set; }

        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }
        
        public DateTime? CompletedTime { get; set; }
        
        public DateTime? CanceledTime { get; set; }
        
        #endregion
        
        public List<RefundItemDto> RefundItems { get; set; }
    }
}
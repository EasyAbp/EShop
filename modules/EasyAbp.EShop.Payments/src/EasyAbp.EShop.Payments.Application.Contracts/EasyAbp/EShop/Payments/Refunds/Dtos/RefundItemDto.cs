using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    [Serializable]
    public class RefundItemDto : ExtensibleFullAuditedEntityDto<Guid>, IRefundItem
    {
        #region Base properties

        public Guid PaymentItemId { get; set; }

        public decimal RefundAmount { get; set; }

        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }
        
        #endregion
        
        public Guid StoreId { get; set; }
        
        public Guid OrderId { get; set; }
        
        public List<RefundItemOrderLineDto> OrderLines { get; set; }
        
        public List<RefundItemOrderExtraFeeEto> OrderExtraFees { get; set; }
    }
}
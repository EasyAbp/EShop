﻿using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Serializable]
    public class EShopRefundEto : IRefund, IMultiTenant
    {
        #region Base properties

        public Guid Id { get; set; }

        public Guid? TenantId { get; set; }

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

        public ExtraPropertyDictionary ExtraProperties { get; set; }
        
        #endregion

        public List<EShopRefundItemEto> RefundItems { get; set; } = new List<EShopRefundItemEto>();
    }
}
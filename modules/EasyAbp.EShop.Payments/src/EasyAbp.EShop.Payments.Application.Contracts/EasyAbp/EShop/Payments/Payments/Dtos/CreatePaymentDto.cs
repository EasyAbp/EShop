﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    public class CreatePaymentDto
    {
        [DisplayName("PaymentPaymentMethod")]
        public string PaymentMethod { get; set; }

        [DisplayName("PaymentCurrency")]
        public string Currency { get; set; }
        
        [DisplayName("PaymentExtraProperties")]
        public Dictionary<string, object> ExtraProperties { get; set; }

        [DisplayName("PaymentItem")]
        public List<CreatePaymentItemDto> PaymentItems { get; set; }
    }
}
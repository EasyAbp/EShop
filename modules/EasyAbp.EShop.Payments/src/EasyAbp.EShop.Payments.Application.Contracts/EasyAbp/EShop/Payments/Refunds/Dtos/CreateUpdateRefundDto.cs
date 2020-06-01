using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class CreateUpdateRefundDto
    {
        [DisplayName("RefundStoreId")]
        public Guid? StoreId { get; set; }
    }
}
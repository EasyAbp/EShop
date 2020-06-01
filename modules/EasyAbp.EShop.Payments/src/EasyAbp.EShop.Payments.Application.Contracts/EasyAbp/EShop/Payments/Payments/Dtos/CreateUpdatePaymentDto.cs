using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    public class CreateUpdatePaymentDto
    {
        public Guid? StoreId { get; set; }
    }
}
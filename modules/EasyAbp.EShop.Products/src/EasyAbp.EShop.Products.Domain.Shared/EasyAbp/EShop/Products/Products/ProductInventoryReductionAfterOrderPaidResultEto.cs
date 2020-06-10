using System;

namespace EasyAbp.EShop.Products.Products
{
    [Serializable]
    public class ProductInventoryReductionAfterOrderPaidResultEto
    {
        public Guid? TenantId { get; set; }

        public Guid OrderId { get; set; }
        
        public Guid PaymentId { get; set; }
        
        public Guid PaymentItemId { get; set; }
        
        public bool IsSuccess { get; set; }
    }
}
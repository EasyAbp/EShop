using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    public class CreateOrderDto : IValidatableObject
    {
        [DisplayName("OrderStoreId")]
        public Guid StoreId { get; set; }

        [DisplayName("OrderCustomerRemark")]
        public string CustomerRemark { get; set; }

        [DisplayName("OrderLine")]
        public List<CreateOrderLineDto> OrderLines { get; set; }
        
        [DisplayName("OrderExtraProperties")]
        public Dictionary<string, object> ExtraProperties { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrderLines.Count == 0)
            {
                yield return new ValidationResult(
                    "OrderLines should not be empty.",
                    new[] { "OrderLines" }
                );
            }
            
            if (OrderLines.Any(orderLine => orderLine.Quantity <= 0))
            {
                yield return new ValidationResult(
                    "Quantity should be greater than 0.",
                    new[] { "OrderLines" }
                );
            }
        }
    }
}
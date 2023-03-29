using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyAbp.EShop.Orders.Localization;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    [Serializable]
    public class CreateOrderDto : ExtensibleObject, ICreateOrderInfo
    {
        [DisplayName("OrderStoreId")]
        public Guid StoreId { get; set; }

        [DisplayName("OrderCustomerRemark")]
        public string CustomerRemark { get; set; }

        IEnumerable<ICreateOrderLineInfo> ICreateOrderInfo.OrderLines => OrderLines;

        [DisplayName("OrderLine")]
        public List<CreateOrderLineDto> OrderLines { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var result in base.Validate(validationContext))
            {
                yield return result;
            }

            var localizer = validationContext.GetRequiredService<IStringLocalizer<OrdersResource>>();
            if (OrderLines.Count == 0)
            {
                yield return new ValidationResult(
                    localizer[OrdersErrorCodes.OrderLinesShouldNotBeEmpty],
                    new[] { "OrderLines" }
                );
            }

            if (OrderLines.Any(orderLine => orderLine.Quantity < 1))
            {
                yield return new ValidationResult(
                    localizer[OrdersErrorCodes.QuantityShouldBeGreaterThanZero],
                    new[] { "OrderLines" }
                );
            }
        }
    }
}
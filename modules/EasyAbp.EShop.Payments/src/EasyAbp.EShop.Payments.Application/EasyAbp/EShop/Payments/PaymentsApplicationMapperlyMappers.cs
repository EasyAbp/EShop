using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Payments
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class PaymentToPaymentDtoMapper : MapperBase<Payment, PaymentDto>
    {
        public override partial PaymentDto Map(Payment source);

        public override partial void Map(Payment source, PaymentDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class PaymentItemToPaymentItemDtoMapper : MapperBase<PaymentItem, PaymentItemDto>
    {
        public override partial PaymentItemDto Map(PaymentItem source);

        public override partial void Map(PaymentItem source, PaymentItemDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class RefundToRefundDtoMapper : MapperBase<Refund, RefundDto>
    {
        public override partial RefundDto Map(Refund source);

        public override partial void Map(Refund source, RefundDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class RefundItemToRefundItemDtoMapper : MapperBase<RefundItem, RefundItemDto>
    {
        public override partial RefundItemDto Map(RefundItem source);

        public override partial void Map(RefundItem source, RefundItemDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class RefundItemOrderLineToRefundItemOrderLineDtoMapper : MapperBase<RefundItemOrderLine, RefundItemOrderLineDto>
    {
        public override partial RefundItemOrderLineDto Map(RefundItemOrderLine source);

        public override partial void Map(RefundItemOrderLine source, RefundItemOrderLineDto destination);
    }
}

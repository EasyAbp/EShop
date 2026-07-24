using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Orders
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class OrderToOrderDtoMapper : MapperBase<Order, OrderDto>
    {
        public override partial OrderDto Map(Order source);

        public override partial void Map(Order source, OrderDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class OrderLineToOrderLineDtoMapper : MapperBase<OrderLine, OrderLineDto>
    {
        public override partial OrderLineDto Map(OrderLine source);

        public override partial void Map(OrderLine source, OrderLineDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class OrderDiscountToOrderDiscountDtoMapper : MapperBase<OrderDiscount, OrderDiscountDto>
    {
        public override partial OrderDiscountDto Map(OrderDiscount source);

        public override partial void Map(OrderDiscount source, OrderDiscountDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class OrderExtraFeeToOrderExtraFeeDtoMapper : MapperBase<OrderExtraFee, OrderExtraFeeDto>
    {
        public override partial OrderExtraFeeDto Map(OrderExtraFee source);

        public override partial void Map(OrderExtraFee source, OrderExtraFeeDto destination);
    }
}

using EasyAbp.EShop.Orders.Orders;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Orders
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class OrderToOrderEtoMapper : MapperBase<Order, OrderEto>
    {
        public override partial OrderEto Map(Order source);

        public override partial void Map(Order source, OrderEto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class OrderLineToOrderLineEtoMapper : MapperBase<OrderLine, OrderLineEto>
    {
        public override partial OrderLineEto Map(OrderLine source);

        public override partial void Map(OrderLine source, OrderLineEto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class OrderDiscountToOrderDiscountEtoMapper : MapperBase<OrderDiscount, OrderDiscountEto>
    {
        public override partial OrderDiscountEto Map(OrderDiscount source);

        public override partial void Map(OrderDiscount source, OrderDiscountEto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class OrderExtraFeeToOrderExtraFeeEtoMapper : MapperBase<OrderExtraFee, OrderExtraFeeEto>
    {
        public override partial OrderExtraFeeEto Map(OrderExtraFee source);

        public override partial void Map(OrderExtraFee source, OrderExtraFeeEto destination);
    }
}

using EasyAbp.EShop.Orders.Orders;
using AutoMapper;

namespace EasyAbp.EShop.Orders
{
    public class OrdersDomainAutoMapperProfile : Profile
    {
        public OrdersDomainAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Order, OrderEto>();
            CreateMap<OrderLine, OrderLineEto>();
            CreateMap<OrderExtraFee, OrderExtraFeeEto>();
        }
    }
}

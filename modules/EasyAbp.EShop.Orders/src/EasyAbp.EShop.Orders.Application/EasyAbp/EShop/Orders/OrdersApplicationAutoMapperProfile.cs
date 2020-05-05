using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Orders
{
    public class OrdersApplicationAutoMapperProfile : Profile
    {
        public OrdersApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Order, OrderDto>();
            CreateMap<OrderLine, OrderLineDto>();
        }
    }
}

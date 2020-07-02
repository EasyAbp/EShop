using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderAppService :
        ICrudAppService< 
            OrderDto, 
            Guid, 
            GetOrderListDto,
            CreateOrderDto>
    {
        Task<OrderDto> GetByOrderNumberAsync(string orderNumber);
    }
}
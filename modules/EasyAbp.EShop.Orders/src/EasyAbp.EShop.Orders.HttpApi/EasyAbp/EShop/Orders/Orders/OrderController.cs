using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Orders.Orders
{
    [RemoteService(Name = "EShopOrders")]
    [Route("/api/eShop/orders/order")]
    public class OrderController : OrdersController, IOrderAppService
    {
        private readonly IOrderAppService _service;

        public OrderController(IOrderAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<OrderDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}/abandoned")]
        [RemoteService(false)]
        public Task<OrderDto> UpdateAsync(Guid id, CreateOrderDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}/abandoned")]
        [RemoteService(false)]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("byOrderNumber/{orderNumber}")]
        public Task<OrderDto> GetByOrderNumberAsync(string orderNumber)
        {
            return _service.GetByOrderNumberAsync(orderNumber);
        }

        [HttpPost]
        [Route("{id}/complete")]
        public Task<OrderDto> CompleteAsync(Guid id)
        {
            return _service.CompleteAsync(id);
        }
    }
}

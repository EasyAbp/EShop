using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    [RemoteService(Name = "EasyAbpEShopPluginsBaskets")]
    [Route("/api/e-shop/plugins/baskets/basket-item")]
    public class BasketItemController : BasketsController, IBasketItemAppService
    {
        private readonly IBasketItemAppService _service;

        public BasketItemController(IBasketItemAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<BasketItemDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<BasketItemDto>> GetListAsync(GetBasketItemListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<BasketItemDto> CreateAsync(CreateBasketItemDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<BasketItemDto> UpdateAsync(Guid id, UpdateBasketItemDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("in-bulk")]
        public Task DeleteInBulkAsync(IEnumerable<Guid> ids)
        {
            return _service.DeleteInBulkAsync(ids);
        }
    }
}
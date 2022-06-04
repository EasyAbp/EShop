using System;
using EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores
{
    [RemoteService(Name = "BookingGrantedStore")]
    [Route("/api/booking/store-asset-category")]
    public class GrantedStoreController : BookingController, IGrantedStoreAppService
    {
        private readonly IGrantedStoreAppService _service;

        public GrantedStoreController(IGrantedStoreAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public virtual Task<GrantedStoreDto> CreateAsync(CreateUpdateGrantedStoreDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<GrantedStoreDto> UpdateAsync(Guid id, CreateUpdateGrantedStoreDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<GrantedStoreDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("")]
        public virtual Task<PagedResultDto<GrantedStoreDto>> GetListAsync(GetGrantedStoreListDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}
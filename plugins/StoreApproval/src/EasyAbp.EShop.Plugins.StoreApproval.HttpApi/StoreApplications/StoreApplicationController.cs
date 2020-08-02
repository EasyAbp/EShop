using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications.Dtos;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    [RemoteService(Name = "StoreApprovalStoreApplication")]
    [Route("/api/eShop/storeApproval/storeApplication")]
    public class StoreApplicationController : StoreApprovalController, IStoreApplicationAppService
    {
        private readonly IStoreApplicationAppService _service;

        public StoreApplicationController(IStoreApplicationAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<StoreApplicationDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<StoreApplicationDto>> GetListAsync(GetStoreApplicationListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public virtual Task<StoreApplicationDto> CreateAsync(CreateStoreApplicationDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<StoreApplicationDto> UpdateAsync(Guid id, UpdateStoreApplicationDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpPost]
        [Route("{id}/submit")]
        public virtual Task<StoreApplicationDto> SubmitAsync(Guid id)
        {
            return _service.SubmitAsync(id);
        }

        [HttpPost]
        [Route("{id}/approve")]
        public virtual Task<StoreApplicationDto> ApproveAsync(Guid id)
        {
            return _service.ApproveAsync(id);
        }

        [HttpPost]
        [Route("{id}/reject")]
        public virtual Task<StoreApplicationDto> RejectAsync(Guid id)
        {
            return _service.RejectAsync(id);
        }
    }
}
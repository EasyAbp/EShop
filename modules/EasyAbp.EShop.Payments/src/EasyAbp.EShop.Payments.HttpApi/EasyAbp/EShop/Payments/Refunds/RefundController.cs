using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds
{
    [RemoteService(Name = EShopPaymentsRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/e-shop/payments/refund")]
    public class RefundController : PaymentsController, IRefundAppService
    {
        private readonly IRefundAppService _service;

        public RefundController(IRefundAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<RefundDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<RefundDto>> GetListAsync(GetRefundListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task CreateAsync(CreateEShopRefundInput input)
        {
            return _service.CreateAsync(input);
        }
    }
}
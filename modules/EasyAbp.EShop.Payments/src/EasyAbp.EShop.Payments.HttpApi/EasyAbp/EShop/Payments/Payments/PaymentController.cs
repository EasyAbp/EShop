using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Payments
{
    [RemoteService(Name = "EasyAbpEShopPayments")]
    [Route("/api/e-shop/payments/payment")]
    public class PaymentController : PaymentsController, IPaymentAppService
    {
        private readonly IPaymentAppService _service;

        public PaymentController(IPaymentAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<PaymentDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<PaymentDto>> GetListAsync(GetPaymentListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task CreateAsync(CreatePaymentDto input)
        {
            return _service.CreateAsync(input);
        }
    }
}
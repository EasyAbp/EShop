using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentAppService :
        IReadOnlyAppService< 
            PaymentDto, 
            Guid, 
            GetPaymentListDto>
    {
        Task CreateAsync(CreatePaymentDto input);
    }
}
using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentAppService :
        ICrudAppService< 
            PaymentDto, 
            Guid, 
            GetPaymentListDto,
            object,
            object>
    {
        Task CreateAsync(CreatePaymentDto input);
    }
}
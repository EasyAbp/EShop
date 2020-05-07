using System;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentAppService :
        ICrudAppService< 
            PaymentDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreatePaymentDto,
            object>
    {

    }
}
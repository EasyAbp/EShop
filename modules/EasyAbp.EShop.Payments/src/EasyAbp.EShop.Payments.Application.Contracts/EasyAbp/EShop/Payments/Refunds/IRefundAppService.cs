using System;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.Refunds
{
    public interface IRefundAppService :
        IReadOnlyAppService< 
            RefundDto, 
            Guid, 
            GetRefundListDto>
    {

    }
}
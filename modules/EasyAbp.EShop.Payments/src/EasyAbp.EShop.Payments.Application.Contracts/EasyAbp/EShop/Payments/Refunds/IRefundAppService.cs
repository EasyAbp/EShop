using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.Refunds
{
    public interface IRefundAppService :
        IReadOnlyAppService< 
            RefundDto, 
            Guid, 
            GetRefundListDto>
    {
        Task CreateAsync(CreateEShopRefundInput input);
    }
}
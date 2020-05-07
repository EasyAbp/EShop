using System;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundAppService : CrudAppService<Refund, RefundDto, Guid, PagedAndSortedResultRequestDto, CreateRefundDto, object>,
        IRefundAppService
    {
        private readonly IRefundRepository _repository;

        public RefundAppService(IRefundRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
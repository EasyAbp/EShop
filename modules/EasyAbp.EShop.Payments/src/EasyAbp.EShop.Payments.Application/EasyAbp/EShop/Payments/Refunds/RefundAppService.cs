using System;
using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundAppService : CrudAppService<Refund, RefundDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateRefundDto, CreateUpdateRefundDto>,
        IRefundAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Refunds.Default;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Refunds.Default;
        protected override string CreatePolicyName { get; set; } = PaymentsPermissions.Refunds.Create;
        protected override string UpdatePolicyName { get; set; } = PaymentsPermissions.Refunds.Update;
        protected override string DeletePolicyName { get; set; } = PaymentsPermissions.Refunds.Delete;

        private readonly IRefundRepository _repository;
        
        public RefundAppService(IRefundRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
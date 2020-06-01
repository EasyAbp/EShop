using System;
using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentAppService : CrudAppService<Payment, PaymentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdatePaymentDto, CreateUpdatePaymentDto>,
        IPaymentAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Payments.Default;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Payments.Default;
        protected override string CreatePolicyName { get; set; } = PaymentsPermissions.Payments.Create;
        protected override string UpdatePolicyName { get; set; } = PaymentsPermissions.Payments.Update;
        protected override string DeletePolicyName { get; set; } = PaymentsPermissions.Payments.Delete;

        private readonly IPaymentRepository _repository;
        
        public PaymentAppService(IPaymentRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
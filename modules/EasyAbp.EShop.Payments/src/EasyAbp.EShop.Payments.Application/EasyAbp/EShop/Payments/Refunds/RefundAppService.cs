using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using EasyAbp.EShop.Stores.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Authorize]
    public class RefundAppService : ReadOnlyAppService<Refund, RefundDto, Guid, GetRefundListDto>,
        IRefundAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Refunds.Default;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Refunds.Default;

        private readonly IPaymentRepository _paymentRepository;
        private readonly IRefundRepository _repository;
        
        public RefundAppService(
            IPaymentRepository paymentRepository,
            IRefundRepository repository) : base(repository)
        {
            _paymentRepository = paymentRepository;
            _repository = repository;
        }

        public override async Task<RefundDto> GetAsync(Guid id)
        {
            var refund = await base.GetAsync(id);

            var payment = await _paymentRepository.GetAsync(refund.PaymentId);
            
            if (payment.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(PaymentsPermissions.Refunds.Manage);
            }

            return refund;
        }
        
        protected override IQueryable<Refund> CreateFilteredQuery(GetRefundListDto input)
        {
            var query = input.UserId.HasValue ? _repository.GetQueryableByUserId(input.UserId.Value) : _repository;

            return query;
        }

        public override async Task<PagedResultDto<RefundDto>> GetListAsync(GetRefundListDto input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(PaymentsPermissions.Refunds.Manage);
            }

            return await base.GetListAsync(input);
        }
    }
}
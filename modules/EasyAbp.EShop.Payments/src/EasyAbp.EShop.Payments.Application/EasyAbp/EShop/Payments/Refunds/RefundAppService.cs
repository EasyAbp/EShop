using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Authorize]
    public class RefundAppService : CrudAppService<Refund, RefundDto, Guid, GetRefundListDto, object, object>,
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

                // Todo: Check if current user is an admin of the store.
            }

            return refund;
        }
        
        protected override IQueryable<Refund> CreateFilteredQuery(GetRefundListDto input)
        {
            var query = input.UserId.HasValue ? _repository.GetQueryableByUserId(input.UserId.Value) : _repository;

            if (input.StoreId.HasValue)
            {
                query = query.Where(x => x.StoreId == input.StoreId.Value);
            }

            return query;
        }

        public override async Task<PagedResultDto<RefundDto>> GetListAsync(GetRefundListDto input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(PaymentsPermissions.Refunds.Manage);

                if (input.StoreId.HasValue)
                {
                    // Todo: Check if current user is an admin of the store.
                }
                else
                {
                    await AuthorizationService.CheckAsync(PaymentsPermissions.Refunds.CrossStore);
                }
            }

            return await base.GetListAsync(input);
        }

        [RemoteService(false)]
        public override Task<RefundDto> CreateAsync(object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task<RefundDto> UpdateAsync(Guid id, object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            throw new NotSupportedException();
        }
    }
}
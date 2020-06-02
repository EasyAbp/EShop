using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Payments
{
    [Authorize]
    public class PaymentAppService : CrudAppService<Payment, PaymentDto, Guid, GetPaymentListDto, object, object>,
        IPaymentAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Payments.Default;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Payments.Default;

        private readonly IPaymentRepository _repository;
        
        public PaymentAppService(IPaymentRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<PaymentDto> GetAsync(Guid id)
        {
            var payment = await base.GetAsync(id);

            if (payment.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(PaymentsPermissions.Payments.Manage);

                // Todo: Check if current user is an admin of the store.
            }

            return payment;
        }
        
        protected override IQueryable<Payment> CreateFilteredQuery(GetPaymentListDto input)
        {
            var query = base.CreateFilteredQuery(input);

            if (input.StoreId.HasValue)
            {
                query = query.Where(x => x.StoreId == input.StoreId.Value);
            }

            return query;
        }

        public override async Task<PagedResultDto<PaymentDto>> GetListAsync(GetPaymentListDto input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(PaymentsPermissions.Payments.Manage);

                if (input.StoreId.HasValue)
                {
                    // Todo: Check if current user is an admin of the store.
                }
                else
                {
                    await AuthorizationService.CheckAsync(PaymentsPermissions.Payments.CrossStore);
                }
            }

            return await base.GetListAsync(input);
        }

        [RemoteService(false)]
        public override Task<PaymentDto> CreateAsync(object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task<PaymentDto> UpdateAsync(Guid id, object input)
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
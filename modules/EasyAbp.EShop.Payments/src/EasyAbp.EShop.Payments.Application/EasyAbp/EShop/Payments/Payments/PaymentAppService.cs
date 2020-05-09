using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentAppService : CrudAppService<Payment, PaymentDto, Guid, PagedAndSortedResultRequestDto, CreatePaymentDto, object>,
        IPaymentAppService
    {
        private readonly IPaymentPayeeAccountProvider _paymentPayeeAccountProvider;
        private readonly IPaymentServiceResolver _paymentServiceResolver;
        private readonly IPaymentRepository _repository;

        public PaymentAppService(
            IPaymentPayeeAccountProvider paymentPayeeAccountProvider,
            IPaymentServiceResolver paymentServiceResolver,
            IPaymentRepository repository) : base(repository)
        {
            _paymentPayeeAccountProvider = paymentPayeeAccountProvider;
            _paymentServiceResolver = paymentServiceResolver;
            _repository = repository;
        }

        public override async Task<PaymentDto> CreateAsync(CreatePaymentDto input)
        {
            await CheckCreatePolicyAsync();

            var providerType = _paymentServiceResolver.GetProviderTypeOrDefault(input.PaymentMethod) ??
                               throw new UnknownPaymentMethodException(input.PaymentMethod);

            var provider = ServiceProvider.GetService(providerType) as IPaymentServiceProvider ??
                           throw new UnknownPaymentMethodException(input.PaymentMethod);

            var paymentItems = input.PaymentItems.Select(inputPaymentItem =>
                new PaymentItem(GuidGenerator.Create(), inputPaymentItem.ItemType, inputPaymentItem.ItemKey,
                    inputPaymentItem.Currency, inputPaymentItem.OriginalPaymentAmount)).ToList();

            if (paymentItems.Select(item => item.Currency).Any(c => c != input.Currency))
            {
                throw new MultiCurrencyNotSupportedException();
            }

            var payment = new Payment(GuidGenerator.Create(), CurrentTenant.Id, CurrentUser.GetId(),
                input.PaymentMethod, input.Currency, paymentItems.Select(item => item.OriginalPaymentAmount).Sum(),
                paymentItems);

            await Repository.InsertAsync(payment, autoSave: true);

            await CheckPayableAsync(payment, input.ExtraProperties);
            
            var payeeConfigurations = await GetPayeeConfigurationsAsync(payment, input.ExtraProperties);

            // Todo: payment discount

            await provider.PayAsync(payment, input.ExtraProperties, payeeConfigurations);

            return MapToGetOutputDto(payment);
        }

        protected virtual Task<Dictionary<string, object>> GetPayeeConfigurationsAsync(Payment payment,
            Dictionary<string, object> inputExtraProperties)
        {
            // Todo: use payee configurations provider.
            // Todo: get store side payee configurations.
            
            var payeeConfigurations = new Dictionary<string, object>();
            
            return Task.FromResult(payeeConfigurations);
        }

        protected virtual async Task CheckPayableAsync(Payment payment, Dictionary<string, object> inputExtraProperties)
        {
            var itemSet = new HashSet<PaymentItem>(payment.PaymentItems);

            foreach (var authorizer in ServiceProvider.GetServices<IPaymentAuthorizer>())
            {
                foreach (var item in itemSet.ToList())
                {
                    if (await authorizer.IsPaymentItemAllowedAsync(payment, item, inputExtraProperties))
                    {
                        itemSet.Remove(item);
                    }
                }
            }

            if (!itemSet.IsNullOrEmpty())
            {
                throw new PaymentItemNotPayableException(itemSet.Select(item => item.ItemKey).ToList());
            }
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
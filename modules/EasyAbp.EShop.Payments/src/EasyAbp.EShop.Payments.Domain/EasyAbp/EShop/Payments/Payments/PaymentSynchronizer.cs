using System;
using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentSynchronizer :
        IDistributedEventHandler<EntityUpdatedEto<PaymentEto>>,
        IDistributedEventHandler<EntityDeletedEto<PaymentEto>>,
        IPaymentSynchronizer,
        ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentSynchronizer(
            IObjectMapper objectMapper,
            IPaymentRepository paymentRepository)
        {
            _objectMapper = objectMapper;
            _paymentRepository = paymentRepository;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<PaymentEto> eventData)
        {
            var payment = await _paymentRepository.FindAsync(eventData.Entity.Id);
            
            if (payment == null)
            {
                payment = _objectMapper.Map<PaymentEto, Payment>(eventData.Entity);

                await _paymentRepository.InsertAsync(payment, true);
            }
            else
            {
                _objectMapper.Map(eventData.Entity, payment);
            }
            
            if (Guid.TryParse(eventData.Entity.GetProperty<string>("StoreId"), out var storeId))
            {
                payment.SetStoreId(storeId);
            }
            
            await _paymentRepository.UpdateAsync(payment, true);
        }

        public virtual async Task HandleEventAsync(EntityDeletedEto<PaymentEto> eventData)
        {
            var payment = await _paymentRepository.FindAsync(eventData.Entity.Id);

            if (payment == null)
            {
                return;
            }
            
            await _paymentRepository.DeleteAsync(payment, true);
        }
    }
}

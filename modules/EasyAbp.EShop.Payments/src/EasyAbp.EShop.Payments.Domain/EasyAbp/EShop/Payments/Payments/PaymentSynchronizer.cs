using System;
using System.Collections.Generic;
using System.Linq;
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

                payment.SetPaymentItems(
                    _objectMapper.Map<List<PaymentItemEto>, List<PaymentItem>>(eventData.Entity.PaymentItems));

                await _paymentRepository.InsertAsync(payment, true);
            }
            else
            {
                _objectMapper.Map(eventData.Entity, payment);

                foreach (var item in eventData.Entity.PaymentItems)
                {
                    var find = payment.PaymentItems.FirstOrDefault(i => i.Id == item.Id);

                    if (find == null)
                    {
                        payment.PaymentItems.Add(_objectMapper.Map<PaymentItemEto, PaymentItem>(item));
                    }
                    else
                    {
                        _objectMapper.Map(item, find);
                    }

                    var etoPaymentItemIds = eventData.Entity.PaymentItems.Select(i => i.Id).ToList();

                    payment.PaymentItems.RemoveAll(i => !etoPaymentItemIds.Contains(i.Id));
                }
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

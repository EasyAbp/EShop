using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
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
        private readonly ICurrentTenant _currentTenant;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentSynchronizer(
            IObjectMapper objectMapper,
            ICurrentTenant currentTenant,
            IPaymentRepository paymentRepository)
        {
            _objectMapper = objectMapper;
            _currentTenant = currentTenant;
            _paymentRepository = paymentRepository;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<PaymentEto> eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);

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

                foreach (var etoItem in eventData.Entity.PaymentItems)
                {
                    var item = payment.PaymentItems.FirstOrDefault(i => i.Id == etoItem.Id);

                    if (item == null)
                    {
                        if (!Guid.TryParse(etoItem.GetProperty<string>("StoreId"), out var storeId))
                        {
                            throw new StoreIdNotFoundException();
                        }
                        
                        item = _objectMapper.Map<PaymentItemEto, PaymentItem>(etoItem);
                        
                        item.SetStoreId(storeId);

                        payment.PaymentItems.Add(item);
                    }
                    else
                    {
                        _objectMapper.Map(etoItem, item);
                    }
                }
                
                var etoPaymentItemIds = eventData.Entity.PaymentItems.Select(i => i.Id).ToList();

                payment.PaymentItems.RemoveAll(i => !etoPaymentItemIds.Contains(i.Id));
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

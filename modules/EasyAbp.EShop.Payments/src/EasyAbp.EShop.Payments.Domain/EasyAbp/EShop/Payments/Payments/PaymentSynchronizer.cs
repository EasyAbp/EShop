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
        IDistributedEventHandler<EntityCreatedEto<PaymentEto>>,
        IDistributedEventHandler<EntityUpdatedEto<PaymentEto>>,
        IDistributedEventHandler<EntityDeletedEto<PaymentEto>>,
        IPaymentSynchronizer,
        ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ICurrentTenant _currentTenant;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        public PaymentSynchronizer(
            IObjectMapper<EShopPaymentsDomainModule> objectMapper,
            ICurrentTenant currentTenant,
            IPaymentRepository paymentRepository,
            IDistributedEventBus distributedEventBus)
        {
            _objectMapper = objectMapper;
            _currentTenant = currentTenant;
            _paymentRepository = paymentRepository;
            _distributedEventBus = distributedEventBus;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<PaymentEto> eventData)
        {
            if (eventData.Entity.PaymentItems.All(item => item.ItemType != PaymentsConsts.PaymentItemType))
            {
                return;
            }
            
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);

            var payment = await _paymentRepository.FindAsync(eventData.Entity.Id);
            
            if (payment != null)
            {
                return;
            }
            
            payment = CreatePayment(eventData.Entity);

            payment.SetPaymentItems(eventData.Entity.PaymentItems.Select(CreatePaymentItem).ToList());

            payment.PaymentItems.ForEach(FillPaymentItemStoreId);

            await _paymentRepository.InsertAsync(payment, true);
            
            if (payment.CompletionTime.HasValue)
            {
                await PublishPaymentCompletedEventAsync(payment);
            }

            if (payment.CanceledTime.HasValue)
            {
                await PublishPaymentCanceledEventAsync(payment);
            }
        }

        protected virtual async Task PublishPaymentCanceledEventAsync(Payment payment)
        {
            await _distributedEventBus.PublishAsync(
                new EShopPaymentCanceledEto(_objectMapper.Map<Payment, EShopPaymentEto>(payment)));
        }

        protected virtual async Task PublishPaymentCompletedEventAsync(Payment payment)
        {
            await _distributedEventBus.PublishAsync(
                new EShopPaymentCompletedEto(_objectMapper.Map<Payment, EShopPaymentEto>(payment)));
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<PaymentEto> eventData)
        {
            if (eventData.Entity.PaymentItems.All(item => item.ItemType != PaymentsConsts.PaymentItemType))
            {
                return;
            }
            
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);

            var payment = await _paymentRepository.FindAsync(eventData.Entity.Id);
            
            if (payment == null)
            {
                return;
            }

            var publishCompletedEvent = eventData.Entity.CompletionTime.HasValue && !payment.CompletionTime.HasValue;

            var publishCanceledEvent = eventData.Entity.CanceledTime.HasValue && !payment.CanceledTime.HasValue;

            UpdatePayment(eventData.Entity, payment);

            foreach (var etoItem in eventData.Entity.PaymentItems)
            {
                var item = payment.PaymentItems.FirstOrDefault(i => i.Id == etoItem.Id);

                if (item == null)
                {
                    item = CreatePaymentItem(etoItem);

                    FillPaymentItemStoreId(item);

                    payment.PaymentItems.Add(item);
                }
                else
                {
                    UpdatePaymentItem(etoItem, item);
                }
            }
                
            var etoPaymentItemIds = eventData.Entity.PaymentItems.Select(i => i.Id).ToList();

            payment.PaymentItems.RemoveAll(i => !etoPaymentItemIds.Contains(i.Id));

            await _paymentRepository.UpdateAsync(payment, true);

            if (publishCompletedEvent)
            {
                await PublishPaymentCompletedEventAsync(payment);
            }

            if (publishCanceledEvent)
            {
                await PublishPaymentCanceledEventAsync(payment);
            }
        }

        protected virtual Payment CreatePayment(PaymentEto eto)
        {
            var payment = new Payment(eto.Id, eto.TenantId, eto.UserId, eto.PaymentMethod, eto.PayeeAccount,
                eto.ExternalTradingCode, eto.Currency, eto.OriginalPaymentAmount, eto.PaymentDiscount,
                eto.ActualPaymentAmount, eto.RefundAmount, eto.PendingRefundAmount, eto.CompletionTime,
                eto.CanceledTime, eto.CreationTime);

            CopyExtraProperties(eto, payment);

            return payment;
        }

        protected virtual void UpdatePayment(PaymentEto eto, Payment payment)
        {
            payment.Update(eto.UserId, eto.PaymentMethod, eto.PayeeAccount, eto.ExternalTradingCode, eto.Currency,
                eto.OriginalPaymentAmount, eto.PaymentDiscount, eto.ActualPaymentAmount, eto.RefundAmount,
                eto.PendingRefundAmount, eto.CompletionTime, eto.CanceledTime);

            CopyExtraProperties(eto, payment);
        }

        protected virtual PaymentItem CreatePaymentItem(PaymentItemEto eto)
        {
            var item = new PaymentItem(eto.Id, eto.ItemType, eto.ItemKey, eto.OriginalPaymentAmount,
                eto.PaymentDiscount, eto.ActualPaymentAmount, eto.RefundAmount, eto.PendingRefundAmount);

            CopyExtraProperties(eto, item);

            return item;
        }

        protected virtual void UpdatePaymentItem(PaymentItemEto eto, PaymentItem item)
        {
            item.Update(eto.ItemType, eto.ItemKey, eto.OriginalPaymentAmount, eto.PaymentDiscount,
                eto.ActualPaymentAmount, eto.RefundAmount, eto.PendingRefundAmount);

            CopyExtraProperties(eto, item);
        }

        protected virtual void CopyExtraProperties(IHasExtraProperties source, IHasExtraProperties destination)
        {
            foreach (var property in source.ExtraProperties)
            {
                destination.SetProperty(property.Key, property.Value);
            }
        }

        protected virtual void FillPaymentItemStoreId(PaymentItem item)
        {
            var storeId = item.GetProperty<Guid?>(nameof(PaymentItem.StoreId));
            
            if (storeId is null)
            {
                throw new StoreIdNotFoundException();
            }
            
            item.SetStoreId(storeId.Value);
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityDeletedEto<PaymentEto> eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Entity.TenantId);
            
            var payment = await _paymentRepository.FindAsync(eventData.Entity.Id);

            if (payment == null)
            {
                return;
            }
            
            await _paymentRepository.DeleteAsync(payment, true);
        }
    }
}

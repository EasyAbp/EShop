using System;
using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Pay.Infrastructure;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    public class EShopWeChatPayHandler : IWeChatPayHandler, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IDataFilter _dataFilter;
        private readonly IPaymentRepository _paymentRepository;

        public EShopWeChatPayHandler(
            IClock clock,
            IDataFilter dataFilter,
            IPaymentRepository paymentRepository)
        {
            _clock = clock;
            _dataFilter = dataFilter;
            _paymentRepository = paymentRepository;
        }
        
        public virtual async Task HandleAsync(XmlDocument xmlDocument)
        {
            using var disabledDataFilter = _dataFilter.Disable<IMultiTenant>();

            if (xmlDocument.Attributes == null || xmlDocument.Attributes["return_code"].Value != "SUCCESS")
            {
                return;
            }
            
            if (xmlDocument.Attributes["result_code"].Value == "SUCCESS")
            {
                var orderId = Guid.Parse(xmlDocument.Attributes["out_trade_no"].Value);
            
                var payment = await _paymentRepository.GetAsync(orderId);
            
                payment.SetExternalTradingCode(xmlDocument.Attributes["transaction_id"].Value);
            
                payment.CompletePayment(_clock.Now);

                await _paymentRepository.UpdateAsync(payment, true);
            }

            // Todo: record xml
        }
    }
}
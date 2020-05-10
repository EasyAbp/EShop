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

            var xml = xmlDocument.SelectSingleNode("xml") ??
                      throw new XmlDocumentMissingRequiredElementException("xml");

            if (xml.SelectSingleNode("return_code")?.InnerText != "SUCCESS")
            {
                return;
            }

            // Todo: sign check
            
            if (xml.SelectSingleNode("result_code")?.InnerText == "SUCCESS")
            {
                var orderId = Guid.Parse(xml.SelectSingleNode("out_trade_no")?.InnerText ??
                                         throw new XmlDocumentMissingRequiredElementException("out_trade_no"));
            
                var payment = await _paymentRepository.GetAsync(orderId);

                payment.SetExternalTradingCode(xml.SelectSingleNode("transaction_id")?.InnerText ??
                                               throw new XmlDocumentMissingRequiredElementException("transaction_id"));

                payment.CompletePayment(_clock.Now);

                await _paymentRepository.UpdateAsync(payment, true);
            }

            // Todo: record xml
        }
    }
}
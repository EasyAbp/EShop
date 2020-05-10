using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Services.Pay;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.WeChatPay.Settings;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    public class WeChatPayPaymentServiceProvider : IPaymentServiceProvider, ITransientDependency
    {
        private readonly ServiceProviderPayService _serviceProviderPayService;
        private readonly IConfiguration _configuration;
        private readonly ISettingProvider _settingProvider;
        private readonly IPaymentOpenIdProvider _paymentOpenIdProvider;
        private readonly IPaymentRepository _paymentRepository;
        
        public const string PaymentMethod = "WeChatPay";
        
        public WeChatPayPaymentServiceProvider(
            ServiceProviderPayService serviceProviderPayService,
            IConfiguration configuration,
            ISettingProvider settingProvider,
            IPaymentOpenIdProvider paymentOpenIdProvider,
            IPaymentRepository paymentRepository)
        {
            _serviceProviderPayService = serviceProviderPayService;
            _configuration = configuration;
            _settingProvider = settingProvider;
            _paymentOpenIdProvider = paymentOpenIdProvider;
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> PayAsync(Payment payment, Dictionary<string, object> inputExtraProperties,
            Dictionary<string, object> payeeConfigurations)
        {
            if (payment.Currency != "CNY")
            {
                throw new CurrencyNotSupportedException(payment.PaymentMethod, payment.Currency);
            }

            var payeeAccount = payeeConfigurations.GetOrDefault("PayeeAccount") as string ??
                               await _settingProvider.GetOrNullAsync(WeChatPaySettings.WeChatPayPaymentMethod
                                   .MchId);
            
            payment.SetPayeeAccount(payeeAccount);
            
            var openId = await _paymentOpenIdProvider.FindUserOpenIdAsync(payment.UserId);
            
            var outTradeNo = payment.Id.ToString("N");

            var result = await _serviceProviderPayService.UnifiedOrderAsync(
                appId: inputExtraProperties.GetOrDefault("appid") as string,
                subAppId: null,
                mchId: payment.PayeeAccount,
                subMchId: null,
                deviceInfo: payeeConfigurations.GetOrDefault("deviceInfo") as string ?? "EasyAbp Payment Service",
                body: payeeConfigurations.GetOrDefault("body") as string ?? "EasyAbp Payment Service",
                detail: payeeConfigurations.GetOrDefault("detail") as string,
                attach: payeeConfigurations.GetOrDefault("attach") as string,
                outTradeNo: outTradeNo,
                feeType: payment.Currency,
                totalFee: ConvertDecimalToWeChatPayFee(payment.ActualPaymentAmount),
                billCreateIp: "127.0.0.1",
                timeStart: null,
                timeExpire: null,
                goodsTag: payeeConfigurations.GetOrDefault("goods_tag") as string,
                notifyUrl: payeeConfigurations.GetOrDefault("notify_url") as string 
                           ?? _configuration["App:SelfUrl"].EnsureEndsWith('/') + "WeChatPay/Notify",
                tradeType: inputExtraProperties.GetOrDefault("trade_type") as string,
                productId: null,
                limitPay: payeeConfigurations.GetOrDefault("limit_pay") as string,
                openId: openId,
                subOpenId: null,
                receipt: payeeConfigurations.GetOrDefault("receipt") as string ?? "N",
                sceneInfo: null);

            var xml = result.SelectSingleNode("xml") ?? throw new UnifiedOrderFailedException();
            
            if (xml.SelectSingleNode("return_code")?.Value != "SUCCESS")
            {
                throw new UnifiedOrderFailedException(xml.SelectSingleNode("return_code")?.Value, xml.SelectSingleNode("return_msg")?.Value);
            }

            if (xml.SelectSingleNode("result_code")?.Value != "SUCCESS")
            {
                throw new UnifiedOrderFailedException(xml.SelectSingleNode("return_code")?.Value,
                    xml.SelectSingleNode("return_msg")?.Value, xml.SelectSingleNode("err_code_des")?.Value,
                    xml.SelectSingleNode("err_code")?.Value);
            }

            payment.SetProperty("trade_type", xml.SelectSingleNode("trade_type"));
            payment.SetProperty("prepay_id", xml.SelectSingleNode("prepay_id"));
            payment.SetProperty("code_url", xml.SelectSingleNode("code_url"));
            
            return await _paymentRepository.UpdateAsync(payment, true);
        }

        private static int ConvertDecimalToWeChatPayFee(decimal paymentActualPaymentAmount)
        {
            return Convert.ToInt32(decimal.Round(paymentActualPaymentAmount, 2, MidpointRounding.AwayFromZero) * 100);
        }
    }
}
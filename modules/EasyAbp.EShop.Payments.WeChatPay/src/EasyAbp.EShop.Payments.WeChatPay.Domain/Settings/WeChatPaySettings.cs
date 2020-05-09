namespace EasyAbp.EShop.Payments.WeChatPay.Settings
{
    public static class WeChatPaySettings
    {
        public const string GroupName = "EasyAbp.EShop.Payments.WeChatPay";

        /* Add constants for setting names. Example:
         * public const string MySettingName = GroupName + ".MySettingName";
         */
        
        public static class WeChatPayPaymentMethod
        {
            private const string PaymentMethodName = GroupName + ".WeChatPay";
            
            public const string MchId = PaymentMethodName + ".MchId";
            public const string ApiKey = PaymentMethodName + ".ApiKey";
            public const string IsSandBox = PaymentMethodName + ".IsSandBox";
            public const string NotifyUrl = PaymentMethodName + ".NotifyUrl";
            public const string RefundNotifyUrl = PaymentMethodName + ".RefundNotifyUrl";
            public const string CertificatePath = PaymentMethodName + ".CertificatePath";
            public const string CertificateSecret = PaymentMethodName + ".CertificateSecret";
        }
    }
}
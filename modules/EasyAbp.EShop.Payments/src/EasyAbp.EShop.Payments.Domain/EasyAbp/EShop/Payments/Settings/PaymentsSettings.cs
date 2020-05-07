namespace EasyAbp.EShop.Payments.Settings
{
    public static class PaymentsSettings
    {
        public const string GroupName = "EasyAbp.EShop.Payments";

        /* Add constants for setting names. Example:
         * public const string MySettingName = GroupName + ".MySettingName";
         */
        
        public static class FreePaymentMethod
        {
            private const string PaymentMethodName = GroupName + ".Free";
            
            public const string DefaultPayeeAccount = PaymentMethodName + ".DefaultPayeeAccount";

        }
    }
}
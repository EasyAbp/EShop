using System;

namespace EasyAbp.EShop.Payments
{
    public static class PaymentsTestData
    {
        public static Guid Order1 { get; } = Guid.NewGuid();

        public static Guid Order2 { get; } = Guid.NewGuid();
        
        public static Guid OrderLine1 { get; } = Guid.NewGuid();

        public static Guid Store1 { get; } = Guid.NewGuid();

        public static Guid Payment1 { get; } = Guid.NewGuid();
        
        public static Guid Payment2 { get; } = Guid.NewGuid();
        
        public static Guid PaymentItem1 { get; } = Guid.NewGuid();
    }
}
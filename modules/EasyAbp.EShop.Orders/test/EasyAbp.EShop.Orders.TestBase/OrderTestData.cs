using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders
{
    public class OrderTestData
    {
        public static Guid Order1Id { get; } = Guid.NewGuid();
        
        public static Guid OrderLine1Id { get; } = Guid.NewGuid();
        
        public static Guid Payment1Id { get; } = Guid.NewGuid();
        
        public static Guid Store1Id { get; } = Guid.NewGuid();

        public static Guid Product1Id { get; } = Guid.NewGuid();

        public static Guid ProductSku1Id { get; } = Guid.NewGuid();
        
        public static Guid ProductSku2Id { get; } = Guid.NewGuid();
        
        public static Guid ProductDetail1Id { get; } = Guid.NewGuid();
        
        public static Guid ProductDetail2Id { get; } = Guid.NewGuid();
        
        public static DateTime ProductLastModificationTime { get; } = DateTime.Today;
        
        public static DateTime ProductDetailLastModificationTime { get; } = DateTime.Today;
    }
}
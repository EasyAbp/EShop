using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders
{
    public class OrderTestData
    {
        public static Guid Store1Id { get; } = Guid.NewGuid();

        public static Guid Product1Id { get; } = Guid.NewGuid();

        public static Guid ProductSku1Id { get; } = Guid.NewGuid();
        
        public static DateTime ProductLastModificationTime { get; } = DateTime.Today;
    }
}
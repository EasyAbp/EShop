using System;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [BackgroundJobName("EShopUnpaidOrderAutoCancel")]
    public class UnpaidOrderAutoCancelArgs : IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        public Guid OrderId { get; set; }
    }
}
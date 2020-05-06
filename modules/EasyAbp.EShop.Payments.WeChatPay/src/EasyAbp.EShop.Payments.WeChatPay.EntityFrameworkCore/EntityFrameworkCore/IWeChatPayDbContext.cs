using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Payments.WeChatPay.EntityFrameworkCore
{
    [ConnectionStringName(WeChatPayDbProperties.ConnectionStringName)]
    public interface IWeChatPayDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderNumberGenerator : IOrderNumberGenerator, ITransientDependency
    {
        private readonly IClock _clock;

        public OrderNumberGenerator(IClock clock)
        {
            _clock = clock;
        }
        
        public virtual Task<string> CreateAsync(Order order)
        {
            return Task.FromResult(_clock.Now.ToString("yyyyMMddHHmmssffff") + RandomHelper.GetRandom(0, 99).ToString("00"));
        }
    }
}
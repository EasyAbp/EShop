using System.Threading.Tasks;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderDiscountProvider
    {
        int EffectOrder { get; }

        Task DiscountAsync(OrderDiscountContext context);
    }
}
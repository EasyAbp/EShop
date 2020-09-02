using System.Threading.Tasks;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderCompletableCheckProvider
    {
        Task CheckAsync(Order order);
    }
}
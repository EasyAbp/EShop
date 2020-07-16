using System.Threading.Tasks;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface ICompletableCheckProvider
    {
        Task CheckAsync(Order order);
    }
}
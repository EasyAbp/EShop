using System.Threading.Tasks;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderNumberGenerator
    {
        Task<string> CreateAsync(Order order);
    }
}
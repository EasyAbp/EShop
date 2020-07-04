using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public interface IBasketItemRepository : IRepository<BasketItem, Guid>
    {
    }
}
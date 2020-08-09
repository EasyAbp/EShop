using System;
using EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class BasketItemRepository : EfCoreRepository<IBasketsDbContext, BasketItem, Guid>, IBasketItemRepository
    {
        public BasketItemRepository(IDbContextProvider<BasketsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
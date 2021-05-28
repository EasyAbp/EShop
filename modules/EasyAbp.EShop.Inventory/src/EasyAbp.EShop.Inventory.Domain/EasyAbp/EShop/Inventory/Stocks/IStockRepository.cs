using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public interface IStockRepository : IRepository<Stock, Guid>
    {
    }
}
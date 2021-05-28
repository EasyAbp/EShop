using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Inventory.Reallocations
{
    public interface IReallocationRepository : IRepository<Reallocation, Guid>
    {
    }
}
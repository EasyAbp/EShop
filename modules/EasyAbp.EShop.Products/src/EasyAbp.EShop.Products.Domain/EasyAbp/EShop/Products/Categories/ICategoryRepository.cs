using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Categories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}
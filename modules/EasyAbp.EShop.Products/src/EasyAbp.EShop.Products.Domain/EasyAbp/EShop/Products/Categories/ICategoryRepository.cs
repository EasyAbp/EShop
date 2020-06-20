using System;
using EasyAbp.Abp.Trees;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Categories
{
    public interface ICategoryRepository : ITreeRepository<Category>
    {
    }
}
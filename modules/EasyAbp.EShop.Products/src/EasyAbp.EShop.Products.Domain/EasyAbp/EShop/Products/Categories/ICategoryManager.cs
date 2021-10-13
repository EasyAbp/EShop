using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Categories
{
    public interface ICategoryManager : IDomainService
    {
        Task<Category> CreateAsync(
            Guid? parentId,
            string uniqueName,
            string displayName,
            string description,
            string mediaResources,
            bool isHidden);
        
        Task UpdateAsync(
            Category entity,
            Guid? parentId,
            string uniqueName,
            string displayName,
            string description,
            string mediaResources,
            bool isHidden);
    }
}
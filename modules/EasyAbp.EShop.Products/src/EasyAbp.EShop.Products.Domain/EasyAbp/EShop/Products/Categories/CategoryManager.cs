using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Categories
{
    public class CategoryManager : DomainService, ICategoryManager
    {
        private readonly ICategoryRepository _repository;

        public CategoryManager(ICategoryRepository repository)
        {
            _repository = repository;
        }
        
        public virtual async Task<Category> CreateAsync(Guid? parentId, string uniqueName, string displayName,
            string description, string mediaResources, bool isHidden)
        {
            if (await _repository.AnyAsync(x => x.UniqueName == uniqueName))
            {
                throw new DuplicateCategoryUniqueNameException(uniqueName);
            }

            return new Category(GuidGenerator.Create(), CurrentTenant.Id, parentId, uniqueName, displayName,
                description, mediaResources, isHidden);
        }

        public virtual async Task UpdateAsync(Category entity, Guid? parentId, string uniqueName, string displayName, string description,
            string mediaResources, bool isHidden)
        {
            if (await _repository.AnyAsync(x => x.UniqueName == uniqueName && x.Id != entity.Id))
            {
                throw new DuplicateCategoryUniqueNameException(uniqueName);
            }
            
            entity.Update(parentId, uniqueName, displayName, description, mediaResources, isHidden);
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.Categories.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Categories
{
    public class CategoryAppService : CrudAppService<Category, CategoryDto, Guid, GetCategoryListDto, CreateUpdateCategoryDto, CreateUpdateCategoryDto>,
        ICategoryAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Categories.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Categories.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Categories.Update;
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;

        private readonly ICategoryRepository _repository;

        public CategoryAppService(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override IQueryable<Category> CreateFilteredQuery(GetCategoryListDto input)
        {
            var query =  base.CreateFilteredQuery(input);
            
            return input.ShowHidden ? query : query.Where(x => !x.IsHidden);
        }

        public override async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
        {
            // Todo: Check if current user is an admin of the store.
            var isCurrentUserStoreAdmin = true && await AuthorizationService.IsGrantedAsync(ProductsPermissions.Categories.Default);
            
            if (input.ShowHidden && !isCurrentUserStoreAdmin)
            {
                throw new NotAllowedToGetCategoryListWithShowHiddenException();
            }
            
            return await base.GetListAsync(input);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories.Dtos;
using EasyAbp.EShop.Products.Permissions;
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

        private readonly ICategoryManager _categoryManager;
        private readonly ICategoryRepository _repository;

        public CategoryAppService(
            ICategoryManager categoryManager,
            ICategoryRepository repository) : base(repository)
        {
            _categoryManager = categoryManager;
            _repository = repository;
        }

        protected override async Task<IQueryable<Category>> CreateFilteredQueryAsync(GetCategoryListDto input)
        {
            var query = (await base.CreateFilteredQueryAsync(input)).Where(x => x.ParentId == input.ParentId);
            
            return input.ShowHidden ? query : query.Where(x => !x.IsHidden);
        }

        protected override async Task<Category> MapToEntityAsync(CreateUpdateCategoryDto createInput)
        {
            return await _categoryManager.CreateAsync(createInput.ParentId, createInput.UniqueName,
                createInput.DisplayName, createInput.Description, createInput.MediaResources, createInput.IsHidden);
        }

        protected override async Task MapToEntityAsync(CreateUpdateCategoryDto updateInput, Category entity)
        {
            await _categoryManager.UpdateAsync(entity, updateInput.ParentId, updateInput.UniqueName,
                updateInput.DisplayName, updateInput.Description, updateInput.MediaResources, updateInput.IsHidden);
        }

        public override async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
        {
            if (input.ShowHidden && !await AuthorizationService.IsGrantedAsync(ProductsPermissions.Categories.ShowHidden))
            {
                throw new NotAllowedToGetCategoryListWithShowHiddenException();
            }
            
            return await base.GetListAsync(input);
        }

        public virtual async Task<PagedResultDto<CategorySummaryDto>> GetSummaryListAsync(GetCategoryListDto input)
        {
            await CheckGetListPolicyAsync();
            
            var query = await _repository.GetQueryableAsync();
            
            var totalCount = await AsyncExecuter.CountAsync(query);

            query = query.OrderBy(x => x.Code);
            
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                query = query.OrderBy(input.Sorting);
            }
            
            query = query.PageBy(input.SkipCount, input.MaxResultCount);

            var categories = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<CategorySummaryDto>(
                totalCount,
                categories.Select(x => ObjectMapper.Map<Category, CategorySummaryDto>(x)).ToList()
            );
        }
    }
}
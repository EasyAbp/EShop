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

        private readonly ICategoryRepository _repository;

        public CategoryAppService(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override IQueryable<Category> CreateFilteredQuery(GetCategoryListDto input)
        {
            var query =  base.CreateFilteredQuery(input).Where(x => x.ParentId == input.ParentId);
            
            return input.ShowHidden ? query : query.Where(x => !x.IsHidden);
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
            
            var query = _repository.AsQueryable();
            
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
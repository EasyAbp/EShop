using System;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.Categories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Categories
{
    public class CategoryAppService : CrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCategoryDto, CreateUpdateCategoryDto>,
        ICategoryAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Categories.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Categories.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Categories.Update;
        protected override string GetPolicyName { get; set; } = ProductsPermissions.Categories.Default;
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Categories.Default;

        private readonly ICategoryRepository _repository;

        public CategoryAppService(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
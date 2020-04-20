using System;
using EasyAbp.EShop.Products.Authorization;
using EasyAbp.EShop.Products.ProductCategories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public class ProductCategoryAppService : CrudAppService<ProductCategory, ProductCategoryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductCategoryDto, CreateUpdateProductCategoryDto>,
        IProductCategoryAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Products.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Products.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Products.Update;
        protected override string GetPolicyName { get; set; } = ProductsPermissions.Products.Default;
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Products.Default;
        
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryAppService(IProductCategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
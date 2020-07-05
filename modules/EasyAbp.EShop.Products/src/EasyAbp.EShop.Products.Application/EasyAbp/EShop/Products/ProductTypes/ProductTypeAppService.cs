using System;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductTypes.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public class ProductTypeAppService : CrudAppService<ProductType, ProductTypeDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductTypeDto, CreateUpdateProductTypeDto>,
        IProductTypeAppService
    {
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.ProductTypes.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.ProductTypes.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.ProductTypes.Update;
        protected override string GetPolicyName { get; set; } = ProductsPermissions.ProductTypes.Default;
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.ProductTypes.Default;
        
        private readonly IProductTypeRepository _repository;

        public ProductTypeAppService(IProductTypeRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
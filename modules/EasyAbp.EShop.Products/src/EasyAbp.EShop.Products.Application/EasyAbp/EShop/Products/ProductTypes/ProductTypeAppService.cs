using System;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductTypes.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public class ProductTypeAppService : ReadOnlyAppService<ProductType, ProductTypeDto, Guid, PagedAndSortedResultRequestDto>,
        IProductTypeAppService
    {
        protected override string GetPolicyName { get; set; } = ProductsPermissions.ProductTypes.Default;
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.ProductTypes.Default;
        
        private readonly IProductTypeRepository _repository;

        public ProductTypeAppService(IProductTypeRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
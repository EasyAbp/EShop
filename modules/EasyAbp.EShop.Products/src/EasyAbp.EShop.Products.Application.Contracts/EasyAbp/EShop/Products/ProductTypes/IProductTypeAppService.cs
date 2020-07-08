using System;
using EasyAbp.EShop.Products.ProductTypes.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public interface IProductTypeAppService :
        IReadOnlyAppService< 
            ProductTypeDto, 
            Guid, 
            PagedAndSortedResultRequestDto>
    {

    }
}
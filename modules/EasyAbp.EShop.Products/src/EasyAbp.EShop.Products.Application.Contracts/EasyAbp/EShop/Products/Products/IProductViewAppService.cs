using System;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductViewAppService :
        IReadOnlyAppService<
            ProductViewDto,
            Guid,
            GetProductListInput>
    {

    }
}
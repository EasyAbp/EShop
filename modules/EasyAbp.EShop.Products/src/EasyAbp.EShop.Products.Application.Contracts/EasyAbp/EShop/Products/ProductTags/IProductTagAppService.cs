using System;
using EasyAbp.EShop.Products.ProductTags.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductTags
{
    public interface IProductTagAppService :
        IReadOnlyAppService<
            ProductTagDto,
            Guid,
            GetProductTagListDto>
    {

    }
}

using System;
using EasyAbp.EShop.Products.Tags.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Tags
{
    public interface ITagAppService : ICrudAppService<
        TagDto,
        Guid,
        GetTagListDto,
        CreateTagDto,
        UpdateTagDto>
    {
    }
}

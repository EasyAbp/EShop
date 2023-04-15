using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public interface IPromotionAppService :
    ICrudAppService<
        PromotionDto,
        Guid,
        PromotionGetListInput,
        CreatePromotionDto,
        UpdatePromotionDto>
{
    Task<ListResultDto<PromotionTypeDto>> GetPromotionTypesAsync();
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public interface IBasketItemAppService :
        ICrudAppService< 
            BasketItemDto, 
            Guid, 
            GetBasketItemListDto,
            CreateBasketItemDto,
            UpdateBasketItemDto>
    {
        Task BatchDeleteAsync(IEnumerable<Guid> ids);

        Task<ListResultDto<ClientSideBasketItemModel>> GenerateClientSideDataAsync(GenerateClientSideDataInput input);
    }
}
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

[IntegrationService]
public interface IPromotionIntegrationService
{
    Task<DiscountProductOutputDto> DiscountProductAsync(DiscountProductInputDto input);

    Task<DiscountOrderOutputDto> DiscountOrderAsync(DiscountOrderInputDto input);
}
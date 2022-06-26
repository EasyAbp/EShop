using System.Linq;
using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults.Dtos;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Plugins.FlashSales;

public class FlashSalesApplicationAutoMapperProfile : Profile, ISingletonDependency
{
    public FlashSalesApplicationAutoMapperProfile(IJsonSerializer jsonSerializer)
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<FlashSalesPlan, FlashSalesPlanDto>()
            .MapExtraProperties();
        CreateMap<FlashSalesPlanDto, FlashSalesPlanEto>()
            .ForMember(x => x.TenantId, opt => opt.Ignore())
            .MapExtraProperties();
        CreateMap<FlashSalesPlanCreateDto, FlashSalesPlan>(MemberList.Source);
        CreateMap<FlashSalesPlanUpdateDto, FlashSalesPlan>(MemberList.Source);
        CreateMap<FlashSalesPlan, FlashSalesPlanCacheItem>()
            .MapExtraProperties();

        CreateMap<FlashSalesResult, FlashSalesResultDto>()
            .MapExtraProperties();

        CreateMap<ProductDto, FlashSalesProductEto>()
            .ForMember(x => x.TenantId, opt => opt.Ignore())
            .MapExtraProperties();
        CreateMap<ProductSkuDto, FlashSalesProductSkuEto>()
            .ForMember(x => x.SerializedAttributeOptionIds, opt => opt.Ignore())
            .AfterMap((src, dest) => dest.SerializedAttributeOptionIds = jsonSerializer.Serialize(src.AttributeOptionIds.OrderBy(x => x)))
            .MapExtraProperties();
        CreateMap<ProductAttributeDto, FlashSalesProductAttributeEto>()
            .MapExtraProperties();
        CreateMap<ProductAttributeOptionDto, FlashSalesProductAttributeOptionEto>()
            .MapExtraProperties();
        CreateMap<ProductDetailDto, FlashSalesProductDetailEto>()
            .MapExtraProperties();
    }
}

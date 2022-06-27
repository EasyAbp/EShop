using System.Linq;
using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Products.Options;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Plugins.FlashSales;

public class EShopProductsPluginsFlashSalesApplicationAutoMapperProfile : Profile, ISingletonDependency
{
    public EShopProductsPluginsFlashSalesApplicationAutoMapperProfile(
        IAttributeOptionIdsSerializer attributeOptionIdsSerializer,
        IOptionsMonitor<EShopProductsOptions> options)
    {
        CreateMap<Product, FlashSalesProductEto>()
            .ForMember(x => x.ProductGroupDisplayName, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                var dict = options.CurrentValue.Groups.GetConfigurationsDictionary();
                dest.ProductGroupDisplayName = dict[src.ProductGroupName].DisplayName;
            })
            .MapExtraProperties();
        CreateMap<ProductSku, FlashSalesProductSkuEto>()
            .ForMember(x => x.AttributeOptionIds, opt => opt.Ignore())
            .AfterMap(async (src, dest) => dest.AttributeOptionIds = (await attributeOptionIdsSerializer.DeserializeAsync(src.SerializedAttributeOptionIds)).ToList())
            .MapExtraProperties();
        CreateMap<ProductAttribute, FlashSalesProductAttributeEto>()
            .MapExtraProperties();
        CreateMap<ProductAttributeOption, FlashSalesProductAttributeOptionEto>()
            .MapExtraProperties();
        CreateMap<ProductDetail, FlashSalesProductDetailEto>()
            .MapExtraProperties();
    }
}

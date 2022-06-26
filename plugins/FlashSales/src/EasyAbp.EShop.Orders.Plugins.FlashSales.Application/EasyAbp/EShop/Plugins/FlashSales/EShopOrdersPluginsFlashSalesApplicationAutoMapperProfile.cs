using System.Collections.Generic;
using System;
using AutoMapper;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Orders
{
    public class EShopOrdersPluginsFlashSalesApplicationAutoMapperProfile : Profile, ISingletonDependency
    {
        public EShopOrdersPluginsFlashSalesApplicationAutoMapperProfile(IJsonSerializer jsonSerializer)
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<FlashSalesProductEto, ProductDto>(MemberList.Source)
                .ForSourceMember(x => x.TenantId, opt => opt.DoNotValidate())
                .MapExtraProperties();
            CreateMap<FlashSalesProductSkuEto, ProductSkuDto>(MemberList.Source)
                .ForMember(x => x.AttributeOptionIds, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.AttributeOptionIds = jsonSerializer.Deserialize<List<Guid>>(src.SerializedAttributeOptionIds))
                .MapExtraProperties();
            CreateMap<FlashSalesProductAttributeEto, ProductAttributeDto>(MemberList.Source)
                .MapExtraProperties();
            CreateMap<FlashSalesProductAttributeOptionEto, ProductAttributeOptionDto>(MemberList.Source)
                .MapExtraProperties();
            CreateMap<FlashSalesProductDetailEto, ProductDetailDto>(MemberList.Source)
                .MapExtraProperties();
        }
    }
}

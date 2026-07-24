using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponDtoToCouponDataMapper : MapperBase<CouponDto, CouponData>
    {
        public override partial CouponData Map(CouponDto source);

        public override partial void Map(CouponDto source, CouponData destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateDtoToCouponTemplateDataMapper : MapperBase<CouponTemplateDto, CouponTemplateData>
    {
        public override partial CouponTemplateData Map(CouponTemplateDto source);

        public override partial void Map(CouponTemplateDto source, CouponTemplateData destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateScopeDtoToCouponTemplateScopeDataMapper : MapperBase<CouponTemplateScopeDto, CouponTemplateScopeData>
    {
        public override partial CouponTemplateScopeData Map(CouponTemplateScopeDto source);

        public override partial void Map(CouponTemplateScopeDto source, CouponTemplateScopeData destination);
    }
}

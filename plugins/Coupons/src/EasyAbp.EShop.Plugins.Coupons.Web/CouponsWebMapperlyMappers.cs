using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.Coupons.Coupon.ViewModels;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate.ViewModels;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplateScope.ViewModels;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Coupons.Web
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateDtoToCreateUpdateCouponTemplateDtoMapper : MapperBase<CouponTemplateDto, CreateUpdateCouponTemplateDto>
    {
        public override partial CreateUpdateCouponTemplateDto Map(CouponTemplateDto source);

        public override partial void Map(CouponTemplateDto source, CreateUpdateCouponTemplateDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateDtoToCreateEditCouponTemplateViewModelMapper : MapperBase<CouponTemplateDto, CreateEditCouponTemplateViewModel>
    {
        public override partial CreateEditCouponTemplateViewModel Map(CouponTemplateDto source);

        public override partial void Map(CouponTemplateDto source, CreateEditCouponTemplateViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateScopeDtoToCreateEditCouponTemplateScopeViewModelMapper : MapperBase<CouponTemplateScopeDto, CreateEditCouponTemplateScopeViewModel>
    {
        public override partial CreateEditCouponTemplateScopeViewModel Map(CouponTemplateScopeDto source);

        public override partial void Map(CouponTemplateScopeDto source, CreateEditCouponTemplateScopeViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditCouponTemplateViewModelToCreateUpdateCouponTemplateDtoMapper : MapperBase<CreateEditCouponTemplateViewModel, CreateUpdateCouponTemplateDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateCouponTemplateDto.Scopes))]
        public override partial CreateUpdateCouponTemplateDto Map(CreateEditCouponTemplateViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateCouponTemplateDto.Scopes))]
        public override partial void Map(CreateEditCouponTemplateViewModel source, CreateUpdateCouponTemplateDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditCouponTemplateScopeViewModelToCreateUpdateCouponTemplateScopeDtoMapper : MapperBase<CreateEditCouponTemplateScopeViewModel, CreateUpdateCouponTemplateScopeDto>
    {
        public override partial CreateUpdateCouponTemplateScopeDto Map(CreateEditCouponTemplateScopeViewModel source);

        public override partial void Map(CreateEditCouponTemplateScopeViewModel source, CreateUpdateCouponTemplateScopeDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateScopeDtoToCreateUpdateCouponTemplateScopeDtoMapper : MapperBase<CouponTemplateScopeDto, CreateUpdateCouponTemplateScopeDto>
    {
        public override partial CreateUpdateCouponTemplateScopeDto Map(CouponTemplateScopeDto source);

        public override partial void Map(CouponTemplateScopeDto source, CreateUpdateCouponTemplateScopeDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponDtoToEditCouponViewModelMapper : MapperBase<CouponDto, EditCouponViewModel>
    {
        public override partial EditCouponViewModel Map(CouponDto source);

        public override partial void Map(CouponDto source, EditCouponViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateCouponViewModelToCreateCouponDtoMapper : MapperBase<CreateCouponViewModel, CreateCouponDto>
    {
        public override partial CreateCouponDto Map(CreateCouponViewModel source);

        public override partial void Map(CreateCouponViewModel source, CreateCouponDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class EditCouponViewModelToUpdateCouponDtoMapper : MapperBase<EditCouponViewModel, UpdateCouponDto>
    {
        public override partial UpdateCouponDto Map(EditCouponViewModel source);

        public override partial void Map(EditCouponViewModel source, UpdateCouponDto destination);
    }
}

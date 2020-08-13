using EasyAbp.EShop.Stores.Stores.Dtos;
using AutoMapper;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels;
using Volo.Abp.AutoMapper;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Stores.Web
{
    public class StoresWebAutoMapperProfile : Profile
    {
        public StoresWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<StoreDto, CreateEditStoreViewModel>()
                .MapExtraProperties(MappingPropertyDefinitionChecks.Both);
            CreateMap<CreateEditStoreViewModel, CreateUpdateStoreDto>(MemberList.Source)
                .MapExtraProperties(MappingPropertyDefinitionChecks.Both);
            
            CreateMap<StoreOwnerDto, CreateEditStoreOwnerViewModel>()
                .MapExtraProperties(MappingPropertyDefinitionChecks.Both);
            CreateMap<CreateEditStoreOwnerViewModel, StoreOwnerDto>(MemberList.Source)
                .MapExtraProperties(MappingPropertyDefinitionChecks.Both);
        }
    }
}

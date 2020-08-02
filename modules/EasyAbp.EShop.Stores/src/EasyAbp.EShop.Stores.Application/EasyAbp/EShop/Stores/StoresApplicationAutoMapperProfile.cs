using AutoMapper;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Stores
{
    public class StoresApplicationAutoMapperProfile : Profile
    {
        public StoresApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Store, StoreDto>()
                .MapExtraProperties(MappingPropertyDefinitionChecks.Both);
            CreateMap<CreateUpdateStoreDto, Store>(MemberList.Source)
                .MapExtraProperties(MappingPropertyDefinitionChecks.Both)
                .ForSourceMember(x => x.OwnerIds, opt => opt.DoNotValidate());

            CreateMap<StoreOwner, StoreOwnerDto>();

        }
    }
}

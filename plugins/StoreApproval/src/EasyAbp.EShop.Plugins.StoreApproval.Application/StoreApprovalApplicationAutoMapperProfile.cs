using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    public class StoreApprovalApplicationAutoMapperProfile : Profile
    {
        public StoreApprovalApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<StoreApplication, StoreApplicationDto>();
            CreateMap<CreateStoreApplicationDto, StoreApplication>(MemberList.Source);
            CreateMap<UpdateStoreApplicationDto, StoreApplication>(MemberList.Source);
        }
    }
}

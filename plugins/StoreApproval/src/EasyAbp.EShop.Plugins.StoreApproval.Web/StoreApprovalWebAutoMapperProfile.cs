using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications.Dtos;
using EasyAbp.EShop.Plugins.StoreApproval.Web.Pages.StoreApproval.StoreApplications.StoreApplication.ViewModels;
using AutoMapper;

namespace EasyAbp.EShop.Plugins.StoreApproval.Web
{
    public class StoreApprovalWebAutoMapperProfile : Profile
    {
        public StoreApprovalWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<StoreApplicationDto, EditStoreApplicationViewModel>();
            CreateMap<CreateStoreApplicationViewModel, CreateStoreApplicationDto>();
            CreateMap<EditStoreApplicationViewModel, UpdateStoreApplicationDto>();
        }
    }
}

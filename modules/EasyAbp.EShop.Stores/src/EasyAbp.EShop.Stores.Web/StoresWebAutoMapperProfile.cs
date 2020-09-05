using EasyAbp.EShop.Stores.Stores.Dtos;
using AutoMapper;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels;
using Volo.Abp.AutoMapper;
using Volo.Abp.ObjectExtending;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Transactions.Transaction.ViewModels;

namespace EasyAbp.EShop.Stores.Web
{
    public class StoresWebAutoMapperProfile : Profile
    {
        public StoresWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<StoreDto, CreateEditStoreViewModel>();
            CreateMap<CreateEditStoreViewModel, CreateUpdateStoreDto>()
                .Ignore(dto => dto.ExtraProperties);
            
            CreateMap<StoreOwnerDto, CreateEditStoreOwnerViewModel>();
            CreateMap<CreateEditStoreOwnerViewModel, CreateUpdateStoreOwnerDto>()
                .Ignore(dto => dto.ExtraProperties);

            CreateMap<TransactionDto, CreateEditTransactionViewModel>();
            CreateMap<CreateEditTransactionViewModel, CreateUpdateTransactionDto>()
                .Ignore(dto => dto.ExtraProperties);
        }
    }
}

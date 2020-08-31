using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.EShop.Stores.Transactions;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Stores
{
    public class StoresApplicationAutoMapperProfile : Profile
    {
        public StoresApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Store, StoreDto>();
            CreateMap<CreateUpdateStoreDto, Store>(MemberList.Source);
            CreateMap<Transaction, TransactionDto>();
            CreateMap<CreateUpdateTransactionDto, Transaction>(MemberList.Source);
        }
    }
}

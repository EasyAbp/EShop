using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.EShop.Stores.Transactions;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Stores
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class StoreToStoreDtoMapper : MapperBase<Store, StoreDto>
    {
        public override partial StoreDto Map(Store source);

        public override partial void Map(Store source, StoreDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class StoreOwnerToStoreOwnerDtoMapper : MapperBase<StoreOwner, StoreOwnerDto>
    {
        [MapperIgnoreTarget(nameof(StoreOwnerDto.OwnerUserName))]
        public override partial StoreOwnerDto Map(StoreOwner source);

        [MapperIgnoreTarget(nameof(StoreOwnerDto.OwnerUserName))]
        public override partial void Map(StoreOwner source, StoreOwnerDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class TransactionToTransactionDtoMapper : MapperBase<Transaction, TransactionDto>
    {
        public override partial TransactionDto Map(Transaction source);

        public override partial void Map(Transaction source, TransactionDto destination);
    }
}

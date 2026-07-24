using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Transactions.Transaction.ViewModels;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Stores.Web
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class StoreDtoToCreateEditStoreViewModelMapper : MapperBase<StoreDto, CreateEditStoreViewModel>
    {
        public override partial CreateEditStoreViewModel Map(StoreDto source);

        public override partial void Map(StoreDto source, CreateEditStoreViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditStoreViewModelToCreateUpdateStoreDtoMapper : MapperBase<CreateEditStoreViewModel, CreateUpdateStoreDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateStoreDto.ExtraProperties))]
        public override partial CreateUpdateStoreDto Map(CreateEditStoreViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateStoreDto.ExtraProperties))]
        public override partial void Map(CreateEditStoreViewModel source, CreateUpdateStoreDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class StoreOwnerDtoToCreateEditStoreOwnerViewModelMapper : MapperBase<StoreOwnerDto, CreateEditStoreOwnerViewModel>
    {
        public override partial CreateEditStoreOwnerViewModel Map(StoreOwnerDto source);

        public override partial void Map(StoreOwnerDto source, CreateEditStoreOwnerViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditStoreOwnerViewModelToCreateUpdateStoreOwnerDtoMapper : MapperBase<CreateEditStoreOwnerViewModel, CreateUpdateStoreOwnerDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateStoreOwnerDto.ExtraProperties))]
        public override partial CreateUpdateStoreOwnerDto Map(CreateEditStoreOwnerViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateStoreOwnerDto.ExtraProperties))]
        public override partial void Map(CreateEditStoreOwnerViewModel source, CreateUpdateStoreOwnerDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class TransactionDtoToCreateEditTransactionViewModelMapper : MapperBase<TransactionDto, CreateEditTransactionViewModel>
    {
        public override partial CreateEditTransactionViewModel Map(TransactionDto source);

        public override partial void Map(TransactionDto source, CreateEditTransactionViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateEditTransactionViewModelToCreateUpdateTransactionDtoMapper : MapperBase<CreateEditTransactionViewModel, CreateUpdateTransactionDto>
    {
        [MapperIgnoreTarget(nameof(CreateUpdateTransactionDto.ExtraProperties))]
        public override partial CreateUpdateTransactionDto Map(CreateEditTransactionViewModel source);

        [MapperIgnoreTarget(nameof(CreateUpdateTransactionDto.ExtraProperties))]
        public override partial void Map(CreateEditTransactionViewModel source, CreateUpdateTransactionDto destination);
    }
}

using EasyAbp.EShop.Stores.Stores.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreAppService : CrudAppService<Store, StoreDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStoreDto, CreateUpdateStoreDto>,
        IStoreAppService
    {
        private readonly IStoreRepository _repository;
        private readonly IStoreManager _storeManager;

        public StoreAppService(IStoreRepository repository,
            IStoreManager storeManager) : base(repository)
        {
            _repository = repository;
            _storeManager = storeManager;
        }

        public async Task<StoreDto> GetDefaultAsync()
        {
            // Todo: need to be improved
            return ObjectMapper.Map<Store, StoreDto>(await _repository.FindDefaultStoreAsync());
        }

        public override async Task<StoreDto> CreateAsync(CreateUpdateStoreDto input)
        {
            await CheckCreatePolicyAsync();

            var entity = MapToEntity(input);

            TryToSetTenantId(entity);

            entity = await _storeManager.CreateAsync(entity, input.OwnerIds);

            return MapToGetOutputDto(entity);
        }

        public override async Task<StoreDto> UpdateAsync(Guid id, CreateUpdateStoreDto input)
        {
            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            MapToEntity(input, entity);

            entity = await _storeManager.UpdateAsync(entity, input.OwnerIds);

            return MapToGetOutputDto(entity);
        }
    }
}
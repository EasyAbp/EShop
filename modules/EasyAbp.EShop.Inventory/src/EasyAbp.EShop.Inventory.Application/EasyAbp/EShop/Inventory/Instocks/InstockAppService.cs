using System;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Inventory.Instocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using EasyAbp.EShop.Stores.Stores;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public class InstockAppService : MultiStoreCrudAppService<Instock, InstockDto, Guid, GetInstockListInput, InstockCreateDto, InstockUpdateDto>,
        IInstockAppService
    {
        protected override string GetPolicyName { get; set; } = InventoryPermissions.Stock.Default;
        protected override string GetListPolicyName { get; set; } = InventoryPermissions.Stock.Default;
        protected override string CreatePolicyName { get; set; } = InventoryPermissions.Stock.Create;
        protected override string UpdatePolicyName { get; set; } = InventoryPermissions.Stock.Update;
        protected override string DeletePolicyName { get; set; } = InventoryPermissions.Stock.Delete;
        protected override string CrossStorePolicyName { get; set; } = InventoryPermissions.Stock.CrossStore;

        private readonly IInstockRepository _repository;

        private readonly IDistributedEventBus _distributedEventBus;

        public InstockAppService(
            IInstockRepository repository, 
            IDistributedEventBus distributedEventBus
         ) : base(repository)
        {
            _repository = repository;

            _distributedEventBus = distributedEventBus;
        }

        public override Task<InstockDto> CreateAsync(InstockCreateDto input)
        {
            //await _distributedEventBus.PublishAsync()

            return base.CreateAsync(input);
        }

        protected override async Task<IQueryable<Instock>> CreateFilteredQueryAsync(GetInstockListInput input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(!input.Filter.IsNullOrEmpty(), s => s.Description.Contains(input.Filter))
                .WhereIf(input.ProductSkuId.HasValue, s => s.ProductSkuId == input.ProductSkuId.Value)
                .WhereIf(input.WarehouseId.HasValue, s => s.WarehouseId == input.WarehouseId.Value)
                .WhereIf(input.StoreId.HasValue, s => s.StoreId == input.StoreId.Value)
                .WhereIf(input.SupplierId.HasValue, s => s.SupplierId == input.SupplierId.Value)
                .WhereIf(input.CreationStartTime.HasValue, s => s.CreationTime.Date >= input.CreationStartTime.Value.Date)
                .WhereIf(input.CreationEndTime.HasValue, s => s.CreationTime.Date >= input.CreationEndTime.Value.Date)
                .WhereIf(input.InstockStartTime.HasValue, s => s.InstockTime.Date >= input.InstockStartTime.Value.Date)
                .WhereIf(input.InstockEndTime.HasValue, s => s.InstockTime.Date >= input.InstockEndTime.Value.Date);
        }
    }
}

using EasyAbp.EShop.Inventory.Settings;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public class WarehouseManager : DomainService, IWarehouseManager
    {
        private readonly IWarehouseRepository _repository;
        private readonly ISettingProvider _settingProvider;

        public WarehouseManager(IWarehouseRepository repository, ISettingProvider settingProvider)
        {
            _repository = repository;
            _settingProvider = settingProvider;
        }

        public async Task<Warehouse> GetDefaultWarehouse(Guid storeId)
        {
            return 
                await _repository.FindDefaultWarehouseAsync(storeId) 
                ?? 
                await _repository.InsertAsync(
                    new Warehouse(GuidGenerator.Create(),
                        await _settingProvider.GetOrNullAsync(InventorySettings.DefaultWarehouseName),
                        null,
                        null,
                        null,
                        null,
                        storeId,
                        CurrentTenant.Id), true);
        }
    }
}
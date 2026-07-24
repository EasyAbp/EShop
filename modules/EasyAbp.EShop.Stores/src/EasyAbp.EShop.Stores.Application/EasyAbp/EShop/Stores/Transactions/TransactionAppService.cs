using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Localization;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Stores.Transactions
{
    public class TransactionAppService : CrudAppService<Transaction, TransactionDto, Guid, GetTransactionListInput, CreateUpdateTransactionDto, CreateUpdateTransactionDto>,
        ITransactionAppService
    {
        protected override string GetPolicyName { get; set; } = StoresPermissions.Transaction.Default;
        protected override string GetListPolicyName { get; set; } = StoresPermissions.Transaction.Default;
        protected override string CreatePolicyName { get; set; } = StoresPermissions.Transaction.Create;
        protected override string UpdatePolicyName { get; set; } = StoresPermissions.Transaction.Update;
        protected override string DeletePolicyName { get; set; } = StoresPermissions.Transaction.Delete;

        private readonly ITransactionRepository _repository;
        
        public TransactionAppService(ITransactionRepository repository) : base(repository)
        {
            _repository = repository;

            LocalizationResource = typeof(StoresResource);
            ObjectMapperContext = typeof(EShopStoresApplicationModule);
        }

        protected override async Task<IQueryable<Transaction>> CreateFilteredQueryAsync(GetTransactionListInput input)
        {
            return (await base.CreateFilteredQueryAsync(input)).Where(x => x.StoreId == input.StoreId);
        }

        protected override Task<Transaction> MapToEntityAsync(CreateUpdateTransactionDto createInput)
        {
            var entity = new Transaction(GuidGenerator.Create(), CurrentTenant.Id, createInput.StoreId,
                createInput.OrderId, createInput.TransactionType, createInput.ActionName, createInput.Currency,
                createInput.Amount);

            createInput.MapExtraPropertiesTo(entity);

            return Task.FromResult(entity);
        }

        protected override Task MapToEntityAsync(CreateUpdateTransactionDto updateInput, Transaction entity)
        {
            entity.Update(updateInput.StoreId, updateInput.OrderId, updateInput.TransactionType,
                updateInput.ActionName, updateInput.Currency, updateInput.Amount);

            updateInput.MapExtraPropertiesTo(entity);

            return Task.CompletedTask;
        }
    }
}

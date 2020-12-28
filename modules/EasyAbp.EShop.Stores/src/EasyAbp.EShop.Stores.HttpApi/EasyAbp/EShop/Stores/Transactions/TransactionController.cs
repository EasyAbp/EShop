using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.Transactions
{
    [RemoteService(Name = "EasyAbpEShopStores")]
    [Route("/api/e-shop/stores/transaction")]
    public class TransactionController : StoresController, ITransactionAppService
    {
        private readonly ITransactionAppService _service;

        public TransactionController(ITransactionAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<TransactionDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<TransactionDto>> GetListAsync(GetTransactionListInput input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<TransactionDto> CreateAsync(CreateUpdateTransactionDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<TransactionDto> UpdateAsync(Guid id, CreateUpdateTransactionDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }
    }
}
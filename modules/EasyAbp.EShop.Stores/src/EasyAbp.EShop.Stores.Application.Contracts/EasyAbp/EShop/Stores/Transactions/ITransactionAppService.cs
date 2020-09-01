using System;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores.Transactions
{
    public interface ITransactionAppService :
        ICrudAppService< 
            TransactionDto, 
            Guid, 
            GetTransactionListInput,
            CreateUpdateTransactionDto,
            CreateUpdateTransactionDto>
    {

    }
}
using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.Transactions.Dtos
{
    public class GetTransactionListInput : PagedAndSortedResultRequestDto
    {
        public Guid StoreId { get; set; }
    }
}
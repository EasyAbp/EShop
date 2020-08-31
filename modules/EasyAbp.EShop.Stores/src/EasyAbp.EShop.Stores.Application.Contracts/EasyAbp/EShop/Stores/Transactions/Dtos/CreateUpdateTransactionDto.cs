using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Stores.Transactions.Dtos
{
    [Serializable]
    public class CreateUpdateTransactionDto
    {
        public Guid StoreId { get; set; }

        public Guid? OrderId { get; set; }

        public TransactionType TransactionType { get; set; }

        public string ActionName { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }
    }
}
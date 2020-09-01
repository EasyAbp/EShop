using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Stores.Transactions;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Transactions.Transaction.ViewModels
{
    public class CreateEditTransactionViewModel
    {
        [Display(Name = "TransactionStoreId")]
        public Guid StoreId { get; set; }

        [Display(Name = "TransactionOrderId")]
        public Guid? OrderId { get; set; }

        [Display(Name = "TransactionTransactionType")]
        public TransactionType TransactionType { get; set; }

        [Display(Name = "TransactionActionName")]
        public string ActionName { get; set; }

        [Display(Name = "TransactionCurrency")]
        public string Currency { get; set; }

        [Display(Name = "TransactionAmount")]
        public decimal Amount { get; set; }
    }
}
using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Transactions;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Transactions.Transaction.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Transactions.Transaction
{
    public class EditModalModel : StoresPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditTransactionViewModel ViewModel { get; set; }

        private readonly ITransactionAppService _service;

        public EditModalModel(ITransactionAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<TransactionDto, CreateEditTransactionViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditTransactionViewModel, CreateUpdateTransactionDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}
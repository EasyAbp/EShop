using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Transactions;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Transactions.Transaction.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Transactions.Transaction
{
    public class CreateModalModel : StoresPageModel
    {
        [BindProperty]
        public CreateEditTransactionViewModel ViewModel { get; set; }

        private readonly ITransactionAppService _service;

        public CreateModalModel(ITransactionAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditTransactionViewModel, CreateUpdateTransactionDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}
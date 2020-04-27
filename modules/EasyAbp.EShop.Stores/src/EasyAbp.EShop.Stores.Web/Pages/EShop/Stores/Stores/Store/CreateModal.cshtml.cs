using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store
{
    public class CreateModalModel : StoresPageModel
    {
        [BindProperty]
        public CreateEditStoreViewModel Store { get; set; }

        private readonly IStoreAppService _service;

        public CreateModalModel(IStoreAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(ObjectMapper.Map<CreateEditStoreViewModel, CreateUpdateStoreDto>(Store));
            return NoContent();
        }
    }
}
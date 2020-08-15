using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store
{
    public class CreateModalModel : StoresPageModel
    {
        [BindProperty] public CreateEditStoreViewModel Store { get; set; }

        private readonly IStoreAppService _service;

        public CreateModalModel(IStoreAppService service)
        {
            _service = service;
        }

        public virtual Task OnGetAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var store = await _service.CreateAsync(
                ObjectMapper.Map<CreateEditStoreViewModel, CreateUpdateStoreDto>(Store));

            return NoContent();
        }
    }
}
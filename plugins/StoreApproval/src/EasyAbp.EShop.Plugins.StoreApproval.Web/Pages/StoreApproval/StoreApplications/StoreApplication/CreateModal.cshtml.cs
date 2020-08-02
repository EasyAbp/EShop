using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications.Dtos;
using EasyAbp.EShop.Plugins.StoreApproval.Web.Pages.StoreApproval.StoreApplications.StoreApplication.ViewModels;

namespace EasyAbp.EShop.Plugins.StoreApproval.Web.Pages.StoreApproval.StoreApplications.StoreApplication
{
    public class CreateModalModel : StoreApprovalPageModel
    {
        [BindProperty]
        public CreateStoreApplicationViewModel ViewModel { get; set; }

        private readonly IStoreApplicationAppService _service;

        public CreateModalModel(IStoreApplicationAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateStoreApplicationViewModel, CreateStoreApplicationDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}
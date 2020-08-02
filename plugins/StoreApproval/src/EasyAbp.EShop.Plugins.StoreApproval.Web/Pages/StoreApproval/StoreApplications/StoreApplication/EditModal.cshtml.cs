using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications.Dtos;
using EasyAbp.EShop.Plugins.StoreApproval.Web.Pages.StoreApproval.StoreApplications.StoreApplication.ViewModels;

namespace EasyAbp.EShop.Plugins.StoreApproval.Web.Pages.StoreApproval.StoreApplications.StoreApplication
{
    public class EditModalModel : StoreApprovalPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public EditStoreApplicationViewModel ViewModel { get; set; }

        private readonly IStoreApplicationAppService _service;

        public EditModalModel(IStoreApplicationAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<StoreApplicationDto, EditStoreApplicationViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditStoreApplicationViewModel, UpdateStoreApplicationDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}
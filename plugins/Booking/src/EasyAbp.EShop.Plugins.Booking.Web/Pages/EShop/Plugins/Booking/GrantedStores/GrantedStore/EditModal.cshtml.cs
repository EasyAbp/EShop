using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.GrantedStores;
using EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.GrantedStores.GrantedStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.GrantedStores.GrantedStore
{
    public class EditModalModel : BookingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditGrantedStoreViewModel ViewModel { get; set; }

        private readonly IGrantedStoreAppService _service;

        public EditModalModel(IGrantedStoreAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<GrantedStoreDto, CreateEditGrantedStoreViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditGrantedStoreViewModel, CreateUpdateGrantedStoreDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}
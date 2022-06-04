using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.GrantedStores;
using EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.GrantedStores.GrantedStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.GrantedStores.GrantedStore
{
    public class CreateModalModel : BookingPageModel
    {
        [BindProperty]
        public CreateEditGrantedStoreViewModel ViewModel { get; set; }

        private readonly IGrantedStoreAppService _service;

        public CreateModalModel(IGrantedStoreAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditGrantedStoreViewModel, CreateUpdateGrantedStoreDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}
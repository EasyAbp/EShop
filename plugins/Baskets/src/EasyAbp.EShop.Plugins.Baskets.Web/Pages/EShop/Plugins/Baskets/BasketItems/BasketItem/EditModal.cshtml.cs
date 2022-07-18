using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using EasyAbp.EShop.Plugins.Baskets.Permissions;
using EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem
{
    public class EditModalModel : BasketsPageModel
    {
        [Required]
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string BasketName { get; set; }
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        
        public bool ServerSide { get; set; }

        [BindProperty]
        public EditBasketItemViewModel ViewModel { get; set; }

        private readonly IBasketItemAppService _service;

        public EditModalModel(IBasketItemAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            ServerSide = await SettingProvider.GetAsync<bool>("EasyAbp.EShop.Plugins.Baskets.EnableServerSideBaskets")
              && await AuthorizationService.IsGrantedAsync(BasketsPermissions.BasketItem.Default);

            if (!ServerSide)
            {
                return;
            }
            
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<BasketItemDto, EditBasketItemViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditBasketItemViewModel, UpdateBasketItemDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}
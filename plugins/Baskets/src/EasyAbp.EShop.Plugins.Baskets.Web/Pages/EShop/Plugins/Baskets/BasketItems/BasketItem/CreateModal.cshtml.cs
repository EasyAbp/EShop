using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem.ViewModels;

namespace EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem
{
    public class CreateModalModel : BasketsPageModel
    {
        [BindProperty]
        public CreateBasketItemViewModel ViewModel { get; set; } = new CreateBasketItemViewModel();

        private readonly IBasketItemAppService _service;

        public CreateModalModel(IBasketItemAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateBasketItemViewModel, CreateBasketItemDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}
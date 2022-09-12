using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using EasyAbp.EShop.Plugins.Baskets.Permissions;
using EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem
{
    public class CreateModalModel : BasketsPageModel
    {
        [BindProperty]
        public CreateBasketItemViewModel ViewModel { get; set; } = new();

        public bool ServerSide { get; set; }

        private readonly IOrderAppService _orderAppService;
        private readonly IBasketItemAppService _basketItemAppService;

        public CreateModalModel(
            IOrderAppService orderAppService,
            IBasketItemAppService basketItemAppService)
        {
            _orderAppService = orderAppService;
            _basketItemAppService = basketItemAppService;
        }

        public virtual async Task OnGetAsync()
        {
            ServerSide = await SettingProvider.GetAsync<bool>("EasyAbp.EShop.Plugins.Baskets.EnableServerSideBaskets")
                         && await AuthorizationService.IsGrantedAsync(BasketsPermissions.BasketItem.Default);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateBasketItemViewModel, CreateBasketItemDto>(ViewModel);

            var checkCreateOrderResult = await _orderAppService.CheckCreateAsync(new CheckCreateOrderInput
            {
                StoreId = ViewModel.StoreId,
                OrderLines = new List<CreateOrderLineDto>
                {
                    new()
                    {
                        ProductId = ViewModel.ProductId,
                        ProductSkuId = ViewModel.ProductSkuId,
                        Quantity = ViewModel.Quantity
                    }
                }
            });

            if (!checkCreateOrderResult.CanCreate)
            {
                throw new BusinessException(BasketsErrorCodes.CheckCreateOrderFailed)
                    .WithData("reason", checkCreateOrderResult.Reason);
            }

            await _basketItemAppService.CreateAsync(dto);

            return NoContent();
        }
    }
}
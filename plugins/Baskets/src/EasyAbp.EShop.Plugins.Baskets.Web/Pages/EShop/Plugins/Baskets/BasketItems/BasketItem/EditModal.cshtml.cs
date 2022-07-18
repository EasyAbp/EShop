using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        private readonly IOrderAppService _orderAppService;
        private readonly IBasketItemAppService _basketItemAppService;

        public EditModalModel(
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

            if (!ServerSide)
            {
                return;
            }
            
            var dto = await _basketItemAppService.GetAsync(Id);
            ViewModel = ObjectMapper.Map<BasketItemDto, EditBasketItemViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var item = await _basketItemAppService.GetAsync(Id);

            var dto = ObjectMapper.Map<EditBasketItemViewModel, UpdateBasketItemDto>(ViewModel);
            
            var checkCreateOrderResult = await _orderAppService.CheckCreateAsync(new CheckCreateOrderInput
            {
                StoreId = item.StoreId,
                OrderLines = new List<CreateOrderLineDto>
                {
                    new()
                    {
                        ProductId = item.ProductId,
                        ProductSkuId = item.ProductSkuId,
                        Quantity = ViewModel.Quantity
                    }
                }
            });

            if (!checkCreateOrderResult.CanCreate)
            {
                throw new BusinessException(BasketsErrorCodes.CheckCreateOrderFailed)
                    .WithData("reason", checkCreateOrderResult.Reason);
            }
            
            await _basketItemAppService.UpdateAsync(Id, dto);
            
            return NoContent();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.EShop.Products.ProductInventories.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku
{
    public class CreateModalModel : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid StoreId { get; set; }
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductId { get; set; }
        
        [BindProperty]
        public CreateProductSkuViewModel ProductSku { get; set; } = new CreateProductSkuViewModel();
        
        [BindProperty]
        public Dictionary<string, Guid> SelectedAttributeOptionIdDict { get; set; }
        
        public Dictionary<string, ICollection<SelectListItem>> Attributes { get; set; }

        private readonly IProductInventoryAppService _productInventoryAppService;
        private readonly IProductAppService _productAppService;

        public CreateModalModel(
            IProductInventoryAppService productInventoryAppService,
            IProductAppService productAppService)
        {
            _productInventoryAppService = productInventoryAppService;
            _productAppService = productAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var product = await _productAppService.GetAsync(ProductId, StoreId);

            Attributes = new Dictionary<string, ICollection<SelectListItem>>();
            
            foreach (var attribute in product.ProductAttributes.ToList())
            {
                Attributes.Add(attribute.DisplayName,
                    attribute.ProductAttributeOptions
                        .Select(dto => new SelectListItem(dto.DisplayName, dto.Id.ToString())).ToList());
            }
        }
        
        public virtual async Task<IActionResult> OnPostAsync()
        {
            var createDto = ObjectMapper.Map<CreateProductSkuViewModel, CreateProductSkuDto>(ProductSku);

            createDto.AttributeOptionIds = SelectedAttributeOptionIdDict.Values.ToList();
            
            var skuDto = await _productAppService.CreateSkuAsync(ProductId, StoreId, createDto);

            await _productInventoryAppService.UpdateAsync(new UpdateProductInventoryDto
            {
                ProductId = ProductId,
                ProductSkuId = skuDto.Id,
                StoreId = StoreId,
                ChangedInventory = ProductSku.Inventory
            });

            return NoContent();
        }
    }
}
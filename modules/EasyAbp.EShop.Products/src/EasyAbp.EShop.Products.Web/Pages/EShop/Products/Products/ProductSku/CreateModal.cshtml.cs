using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.ProductTypes;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;

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
        public CreateEditProductSkuViewModel ProductSku { get; set; }
        
        [BindProperty]
        public Dictionary<string, Guid> SelectedAttributeOptionIdDict { get; set; }
        
        public Dictionary<string, ICollection<SelectListItem>> Attributes { get; set; }
        
        private readonly IProductAppService _productAppService;

        public CreateModalModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var product = await _productAppService.GetAsync(ProductId);

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
            var createDto = ObjectMapper.Map<CreateEditProductSkuViewModel, CreateProductSkuDto>(ProductSku);

            createDto.SerializedAttributeOptionIds = JsonConvert.SerializeObject(SelectedAttributeOptionIdDict.Values);
            
            await _productAppService.CreateSkuAsync(ProductId, StoreId, createDto);

            return NoContent();
        }
    }
}
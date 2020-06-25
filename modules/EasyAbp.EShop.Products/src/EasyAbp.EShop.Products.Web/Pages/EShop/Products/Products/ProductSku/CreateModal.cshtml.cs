using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Json;

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

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProductAppService _productAppService;

        public CreateModalModel(
            IJsonSerializer jsonSerializer,
            IProductAppService productAppService)
        {
            _jsonSerializer = jsonSerializer;
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
            var createDto = ObjectMapper.Map<CreateEditProductSkuViewModel, CreateProductSkuDto>(ProductSku);

            createDto.AttributeOptionIds = SelectedAttributeOptionIdDict.Values.ToList();
            
            await _productAppService.CreateSkuAsync(ProductId, StoreId, createDto);

            return NoContent();
        }
    }
}
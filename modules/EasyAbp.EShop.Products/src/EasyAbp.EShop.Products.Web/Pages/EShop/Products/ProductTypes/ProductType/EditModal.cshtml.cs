using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Products.ProductTypes;
using EasyAbp.EShop.Products.ProductTypes.Dtos;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.ProductTypes.ProductType
{
    public class EditModalModel : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProductTypeDto ProductType { get; set; }

        private readonly IProductTypeAppService _service;

        public EditModalModel(IProductTypeAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ProductType = ObjectMapper.Map<ProductTypeDto, CreateUpdateProductTypeDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, ProductType);
            return NoContent();
        }
    }
}
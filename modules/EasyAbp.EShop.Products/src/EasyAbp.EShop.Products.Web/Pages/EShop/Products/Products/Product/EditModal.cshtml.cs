using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product
{
    public class EditModalModel : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProductDto Product { get; set; }

        private readonly IProductAppService _service;

        public EditModalModel(IProductAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            Product = ObjectMapper.Map<ProductDto, CreateUpdateProductDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, Product);
            return NoContent();
        }
    }
}
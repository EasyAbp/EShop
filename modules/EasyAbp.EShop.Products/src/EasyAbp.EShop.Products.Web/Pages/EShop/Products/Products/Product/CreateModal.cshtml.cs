using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty]
        public CreateUpdateProductDto Product { get; set; }

        private readonly IProductAppService _service;

        public CreateModalModel(IProductAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(Product);
            return NoContent();
        }
    }
}
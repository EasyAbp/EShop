using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductTypes;
using EasyAbp.EShop.Products.ProductTypes.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.ProductTypes.ProductType
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty]
        public CreateUpdateProductTypeDto ProductType { get; set; }

        private readonly IProductTypeAppService _service;

        public CreateModalModel(IProductTypeAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(ProductType);
            return NoContent();
        }
    }
}
using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku
{
    public class IndexModel : ProductsPageModel
    {
        private readonly IProductAppService _productAppService;

        [BindProperty(SupportsGet = true)]
        public Guid ProductId { get; set; }
        
        public string ProductDisplayName { get; set; }
        
        public IndexModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }
        
        public virtual async Task OnGetAsync()
        {
            ProductDisplayName = (await _productAppService.GetAsync(ProductId)).DisplayName;
        }
    }
}

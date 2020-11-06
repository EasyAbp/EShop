using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Categories.Category
{
    public class IndexModel : ProductsPageModel
    {
        private readonly ICategoryAppService _categoryAppService;

        [BindProperty(SupportsGet = true)]
        public Guid? ParentId { get; set; }
        
        public string ParentDisplayName { get; set; }

        public IndexModel(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }
        
        public async Task OnGetAsync()
        {
            if (!ParentId.HasValue)
            {
                return;
            }
            
            ParentDisplayName = (await _categoryAppService.GetAsync(ParentId.Value)).DisplayName;
        }
    }
}

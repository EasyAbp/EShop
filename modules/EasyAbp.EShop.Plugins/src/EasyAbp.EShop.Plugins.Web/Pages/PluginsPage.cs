﻿using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Plugins.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Plugins.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Plugins.Web.Pages.PluginsPage
     */
    public abstract class PluginsPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<PluginsResource> L { get; set; }
    }
}

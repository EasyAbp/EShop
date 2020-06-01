using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace EShopSample.Web
{
    [Dependency(ReplaceServices = true)]
    public class EShopSampleBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "EShopSample";
    }
}

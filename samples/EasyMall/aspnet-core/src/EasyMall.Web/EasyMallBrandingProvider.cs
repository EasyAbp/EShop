using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace EasyMall.Web
{
    [Dependency(ReplaceServices = true)]
    public class EasyMallBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "EasyMall";
    }
}

using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EShopSample.Web
{
    [Dependency(ReplaceServices = true)]
    public class EShopSampleBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "EShopSample";
    }
}

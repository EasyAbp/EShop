using System;
using System.Collections.Generic;
using System.Text;
using EShopSample.Localization;
using Volo.Abp.Application.Services;

namespace EShopSample
{
    /* Inherit your application services from this class.
     */
    public abstract class EShopSampleAppService : ApplicationService
    {
        protected EShopSampleAppService()
        {
            LocalizationResource = typeof(EShopSampleResource);
        }
    }
}

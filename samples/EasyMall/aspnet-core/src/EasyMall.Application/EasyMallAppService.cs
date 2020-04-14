using System;
using System.Collections.Generic;
using System.Text;
using EasyMall.Localization;
using Volo.Abp.Application.Services;

namespace EasyMall
{
    /* Inherit your application services from this class.
     */
    public abstract class EasyMallAppService : ApplicationService
    {
        protected EasyMallAppService()
        {
            LocalizationResource = typeof(EasyMallResource);
        }
    }
}

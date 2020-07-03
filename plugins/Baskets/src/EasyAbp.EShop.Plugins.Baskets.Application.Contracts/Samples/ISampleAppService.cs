using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Baskets.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}

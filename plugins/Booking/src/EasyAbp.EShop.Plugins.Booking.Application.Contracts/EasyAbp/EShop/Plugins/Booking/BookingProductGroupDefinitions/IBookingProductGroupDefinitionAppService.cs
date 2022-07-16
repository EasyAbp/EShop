using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions;

public interface IBookingProductGroupDefinitionAppService : IApplicationService
{
    Task<ListResultDto<BookingProductGroupDefinitionDto>> GetListAsync();
}
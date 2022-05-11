using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions.Dtos;
using EasyAbp.EShop.Plugins.Booking.Options;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions;

public class BookingProductGroupDefinitionAppService : BookingAppService, IBookingProductGroupDefinitionAppService
{
    private readonly EShopBookingOptions _options;

    public BookingProductGroupDefinitionAppService(IOptions<EShopBookingOptions> options)
    {
        _options = options.Value;
    }
    
    public virtual Task<ListResultDto<BookingProductGroupDefinitionDto>> GetListAsync()
    {
        return Task.FromResult(
            new ListResultDto<BookingProductGroupDefinitionDto>(
                _options.BookingProductGroups.Select(x =>
                    new BookingProductGroupDefinitionDto
                    {
                        ProductGroupName = x.ProductGroupName
                    }).ToList()
            )
        );
    }
}
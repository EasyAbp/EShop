using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Booking.Shared;

public class PeriodNotFoundException : BusinessException
{
    public PeriodNotFoundException(Guid id) : base(BookingErrorCodes.PeriodNotFound)
    {
        WithData(nameof(id), id);
    }
}
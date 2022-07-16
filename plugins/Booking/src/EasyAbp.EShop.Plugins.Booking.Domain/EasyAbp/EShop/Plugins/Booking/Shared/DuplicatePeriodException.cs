using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Booking.Shared;

public class DuplicatePeriodException : BusinessException
{
    public DuplicatePeriodException(Guid id) : base(BookingErrorCodes.DuplicatePeriod)
    {
        WithData(nameof(id), id);
    }
}
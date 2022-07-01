using System;

namespace EasyAbp.EShop.Plugins.Booking;

public static class BookingTestConsts
{
    public static string BookingProductGroupName { get; } = "CameraBooking";

    public static Guid Store1Id { get; } = Guid.NewGuid();

    public static Guid Order1Id { get; } = Guid.NewGuid();

    public static Guid OrderLine1Id { get; } = Guid.NewGuid();

    public static Guid OrderLine2Id { get; } = Guid.NewGuid();

    public static Guid BookingProduct1Id { get; } = Guid.NewGuid();

    public static Guid BookingProduct1Sku1Id { get; } = Guid.NewGuid();

    public static Guid Asset1Id { get; } = Guid.NewGuid();

    public static Guid AssetCategory1Id { get; } = Guid.NewGuid();

    public static Guid PeriodScheme1Id { get; } = Guid.NewGuid();

    public static Guid Period1Id { get; } = Guid.NewGuid();

    public static DateTime BookingDate { get; } = DateTime.Today.AddDays(1);

    public static TimeSpan Period1StartingTime { get; } = TimeSpan.FromHours(8); // from 8:00

    public static TimeSpan Period1Duration { get; } = TimeSpan.FromHours(2); // to 10:00 (8:00 + 2h)

    public static int Volume { get; } = 1;
}
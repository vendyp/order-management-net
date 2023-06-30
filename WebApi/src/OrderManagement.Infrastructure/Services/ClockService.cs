using OrderManagement.Core.Abstractions;

namespace OrderManagement.Infrastructure.Services;

public class ClockOptions
{
    public ClockOptions()
    {
        Hours = 7;
    }

    /// <summary>
    /// Default value is 7
    /// </summary>
    public int Hours { get; set; }
}

public class ClockService : IClock
{
    private readonly ClockOptions _options;

    public ClockService(ClockOptions options)
    {
        _options = options;
    }

    public DateTime CurrentDate()
        => DateTime.UtcNow;

    public DateTime CurrentServerDate()
        => CurrentDate().AddHours(_options.Hours);
}
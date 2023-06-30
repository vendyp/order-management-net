namespace OrderManagement.Core.Abstractions;

public interface IContext
{
    Guid RequestId { get; }
    string TraceId { get; }
    string? IpAddress { get; }
    string? UserAgent { get; }
    IIdentityContext Identity { get; }
}
﻿namespace OrderManagement.Core.Abstractions;

public interface IIdentityContext
{
    bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string Username { get; }
    Dictionary<string, IEnumerable<string>> Claims { get; }
    List<string> Roles { get; }
}
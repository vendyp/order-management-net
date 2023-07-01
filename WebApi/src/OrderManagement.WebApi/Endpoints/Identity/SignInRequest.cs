using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.WebApi.Endpoints.Identity;

public class SignInRequest
{
    [FromHeader(Name = "User-Agent")] public string? UserAgent { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
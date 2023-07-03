namespace OrderManagement.WebApi.Dto;

public class SignInDto
{
    public Guid UserId { get; set; }
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public long Expiry { get; set; }
}
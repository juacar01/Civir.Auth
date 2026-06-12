namespace Civir.Auth.Application.Features.Login;

public class LoginVm
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string? Provider { get; set; }
    public string? ProviderUserId { get; set; }

}
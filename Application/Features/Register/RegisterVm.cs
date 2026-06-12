namespace Civir.Auth.Application.Features.Register;

public class RegisterVm
{
    public string Nombre { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

}
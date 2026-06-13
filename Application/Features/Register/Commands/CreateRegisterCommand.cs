using Civir.Utils.Cqrs;

namespace Civir.Auth.Application.Features.Register.Commands;

public class CreateRegisterCommand: IRequest<RegisterVm>
{

    public string Nombre { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

}

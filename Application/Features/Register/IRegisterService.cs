using Civir.Auth.Application.Features.Register.Commands;

namespace Civir.Auth.Application.Features.Register;

public interface IRegisterService
{
    Task<RegisterVm> RegisterAsync(RegisterVm command, CancellationToken cancellationToken);
}
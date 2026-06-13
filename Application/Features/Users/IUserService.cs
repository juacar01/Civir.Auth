namespace Civir.Auth.Application.Features.Users;

public interface IUserService
{
    Task<UserVm> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<UserVm> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}
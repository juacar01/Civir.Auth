using Civir.Utils.Cqrs;

namespace Civir.Auth.Application.Features.Users.Queries;

public class GetUserByEmailQuery: IRequest<UserVm>
{
    public string Email { get; set; }

    public GetUserByEmailQuery(string email)
    { Email = string.IsNullOrWhiteSpace(email) ? throw new ArgumentNullException(nameof(email)) : email; }
}

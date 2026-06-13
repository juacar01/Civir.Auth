using System.Linq.Expressions;
using AutoMapper;
using Civir.Domain.Entities;
using Civir.Infrastructure.Persistence;

namespace Civir.Auth.Application.Features.Users;

public class UserService : IUserService
{

        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserVm> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
          var includes = new List<Expression<Func<User, object>>>();

        var user = await _unitOfWork.Repository<User>().GetEntityAsync(
            a => a.Id == id,
            includes,
            true
        );

        return _mapper.Map<UserVm>(user);
    }

    public async Task<UserVm> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
         var includes = new List<Expression<Func<User, object>>>();

        var user = await _unitOfWork.Repository<User>().GetEntityAsync(
            u => u.Email == email,
            includes,
            true
        );

        return _mapper.Map<UserVm>(user);
    }
}
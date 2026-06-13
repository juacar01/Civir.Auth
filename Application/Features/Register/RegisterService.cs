using AutoMapper;
using Civir.Domain.Entities;
using Civir.Infrastructure.Persistence;
using Civir.Utils.Security;

namespace Civir.Auth.Application.Features.Register;

public class RegisterService : IRegisterService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RegisterVm> RegisterAsync(RegisterVm request, CancellationToken cancellationToken)
    {
        var userEntity = _mapper.Map<User>(request);
        userEntity.PasswordHash = new PasswordHasher().HashPassword(request.Password);
        await _unitOfWork.Repository<User>().AddAsync(userEntity);
        return _mapper.Map<RegisterVm>(userEntity);

    }
}
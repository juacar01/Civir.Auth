using AutoMapper;
using Civir.Domain.Entities;
using Civir.Infrastructure.Persistence;
using Civir.Utils.Cqrs;
using Civir.Utils.Security;

namespace Civir.Auth.Application.Features.Register.Commands;

public class CreateRegisterCommandHandler : IRequestHandler<CreateRegisterCommand, RegisterVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateRegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<RegisterVm> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
    {
        var userEntity = _mapper.Map<User>(request);
        userEntity.PasswordHash = new PasswordHasher().HashPassword(request.Password);
        await _unitOfWork.Repository<User>().AddAsync(userEntity);

        return _mapper.Map<RegisterVm>(userEntity);
    }

}

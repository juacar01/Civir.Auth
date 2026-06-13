using AutoMapper;
using Civir.Auth.Application.Features.Register;
using Civir.Auth.Application.Features.Register.Commands;
using Civir.Auth.Application.Features.Users;
using Civir.Auth.Application.Features.Users.Queries;
using Civir.Auth.Application.Mappings;
using Civir.Utils.Cqrs;
using Microsoft.Extensions.Logging.Abstractions;


namespace Civir.Auth.Application;

public static class ApplicationServiceRegistration
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        var mapperConfig= new MapperConfiguration(cfg =>
        {
            // Add your AutoMapper profiles here
            cfg.AddProfile(new MappingProfile());
        }, NullLoggerFactory.Instance);

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

}
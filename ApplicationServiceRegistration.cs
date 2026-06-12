using AutoMapper;
using Civir.Auth.Application.Beheaviours;
using Civir.Auth.Application.Mappings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

}
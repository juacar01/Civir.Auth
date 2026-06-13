using Civir.Auth.Application.Features.Register;
using Civir.Auth.Application.Features.Register.Commands;
using Civir.Auth.Application.Features.Users;
using Civir.Auth.Application.Features.Users.Queries;
using Civir.Utils.Cqrs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Civir.Auth.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterService _registerService;
    private readonly IUserService _userService;

    public RegisterController(IRegisterService registerService, IUserService userService)
    {
        _registerService = registerService;
        _userService = userService;
    }

    [HttpPost("register", Name = "CreateRegister")]
    [SwaggerOperation(
        Summary = "Registra los datos de un nuevo Autor",
        Description = "Registra los datos de un Nuevo Autor"
    )]
    [ProducesResponseType(typeof(RegisterVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RegisterVm>> CreateRegister([FromBody] RegisterVm request)
    {

        var user = await _userService.GetUserByEmailAsync(request.Email, CancellationToken.None);
        if (user != null)
        {
            return BadRequest("El correo electrónico ya está registrado.");
        }
        
        var author = await _registerService.RegisterAsync(request, CancellationToken.None);
        return Ok(author);
    }

   
}
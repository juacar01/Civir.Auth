using Civir.Auth.Application.Features.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RegisterController : ControllerBase
{
    private IMediator _mediator;

    public RegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create", Name = "CreateAuthor")]
    [SwaggerOperation(
        Summary = "Registra los datos de un nuevo Autor",
        Description = "Registra los datos de un Nuevo Autor"
    )]
    [ProducesResponseType(typeof(RegisterVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RegisterVm>> CreateAuthor([FromBody] CreateAuthorCommand request)
    {
        var author = await _mediator.Send(request);
        return Ok(author);
    }

   
}
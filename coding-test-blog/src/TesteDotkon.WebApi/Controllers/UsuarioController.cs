using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteDotkon.Application.Interfaces;
using TesteDotkon.Domain.Commands.Usuario.Adicionar;
using TesteDotkon.WebApi.Controllers.Base;

namespace TesteDotkon.WebApi.Controllers;

[Route("api/usuario")]
[ApiController]
[Authorize]
public class UsuarioController(IUsuarioAppService usuarioAppService) : BaseApiController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Adicionar(UsuarioAdicionarRequest request)
    {
        var commandResponse = await usuarioAppService.Adicionar(request);
        return RespostaCustomizada(commandResponse);
    }
}

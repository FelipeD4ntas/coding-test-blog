using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TesteDotkon.Application.Interfaces;
using TesteDotkon.Domain.Commands.Postagem.Adicionar;
using TesteDotkon.Domain.Commands.Postagem.Deletar;
using TesteDotkon.Domain.Commands.Postagem.Editar;
using TesteDotkon.Domain.Commands.Postagem.Listar;
using TesteDotkon.WebApi.Controllers.Base;

namespace TesteDotkon.WebApi.Controllers;

[Route("api/postagem")]
[ApiController]
[Authorize]
public class PostagemController(IPostagemAppService postagemAppService) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Adicionar(PostagemAdicionarRequest request)
    {
        var commandResponse = await postagemAppService.Adicionar(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Editar([FromRoute] Guid id, PostagemEditarRequest request)
    {
        request.PostagemId = id;
        var commandResponse = await postagemAppService.Editar(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Deletar([FromRoute] Guid id)
    {
        var request = new PostagemDeletarRequest { Id = id };
        var commandResponse = await postagemAppService.Deletar(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("postagens")]
    [AllowAnonymous]
    public async Task<IActionResult> Listar([FromQuery] PostagemListarRequest request)
    {
        var commandResponse = await postagemAppService.Listar(request);
        return RespostaCustomizada(commandResponse);
    }
}

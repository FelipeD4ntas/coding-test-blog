using Mapster;
using MediatR;
using TesteDotkon.Infra.MediatoR.CommandResponse;
using TesteDotkon.Domain.Interfaces;
using TesteDotkon.Infra.CrossCutting.NotificationPattern;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TesteDotkon.Infra.CrossCutting.Security.Autenticacao;
using TesteDotkon.CrossCutting.WebSockets;

namespace TesteDotkon.Domain.Commands.Postagem.Adicionar;

public class PostagemAdicionarHandler(
    WebSocketService webSocketService,
    IUsuarioAutenticado usuarioAutenticado,
    IRepositoryPostagem repositoryPostagem,
    IValidator<PostagemAdicionarRequest> validator)
    : Notifiable, IRequestHandler<PostagemAdicionarRequest, CommandResponse<PostagemAdicionarResponse>>
{
    public async Task<CommandResponse<PostagemAdicionarResponse>> Handle(PostagemAdicionarRequest request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            AddNotification("Validação", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return await Task.FromResult(new CommandResponse<PostagemAdicionarResponse>(this));
        }

        var postagem = request.Adapt<Entities.Postagem>();
        postagem.DataPublicacao = DateTime.Now;
        postagem.AutorId = usuarioAutenticado.UsuarioId;

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<PostagemAdicionarResponse>(this));

        await repositoryPostagem.AddAsync(postagem, cancellationToken);
        await webSocketService.BroadcastMessage("Novo post publicado!");

        return await Task.FromResult(new CommandResponse<PostagemAdicionarResponse>(new PostagemAdicionarResponse(postagem.Id, "Postagem Feita com sucesso"), this));
    }
}

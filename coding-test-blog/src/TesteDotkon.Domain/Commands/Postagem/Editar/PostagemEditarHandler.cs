using Mapster;
using MediatR;
using TesteDotkon.Infra.MediatoR.CommandResponse;
using TesteDotkon.Domain.Interfaces;
using TesteDotkon.Infra.CrossCutting.NotificationPattern;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TesteDotkon.Infra.CrossCutting.Security.Autenticacao;

namespace TesteDotkon.Domain.Commands.Postagem.Editar;

public class PostagemEditarHandler(
    IUsuarioAutenticado usuarioAutenticado,
    IRepositoryPostagem repositoryPostagem,
    IValidator<PostagemEditarRequest> validator)
    : Notifiable, IRequestHandler<PostagemEditarRequest, CommandResponse<PostagemEditarResponse>>
{
    public async Task<CommandResponse<PostagemEditarResponse>> Handle(PostagemEditarRequest request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            AddNotification("Validação", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return await Task.FromResult(new CommandResponse<PostagemEditarResponse>(this));
        }

        var postagem = await repositoryPostagem.GetByAsync(false, p => p.Id == request.PostagemId, cancellationToken);

        if (postagem is null)
        {
            AddNotification("Postagem", "Postagem Não Encontrada");
            return await Task.FromResult(new CommandResponse<PostagemEditarResponse>(this));
        }

        if (postagem.AutorId != usuarioAutenticado.UsuarioId)
        {
            AddNotification("Postagem", "Postagem Não Pertence ao Usuário");
            return await Task.FromResult(new CommandResponse<PostagemEditarResponse>(this));
        }

        postagem.Editar(request.Titulo, request.Conteudo);

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<PostagemEditarResponse>(this));

        repositoryPostagem.Update(postagem);

        return await Task.FromResult(new CommandResponse<PostagemEditarResponse>(new PostagemEditarResponse(postagem.Id, "Postagem Atualizada com sucesso"), this));
    }
}

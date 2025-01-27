using Mapster;
using MediatR;
using TesteDotkon.Infra.MediatoR.CommandResponse;
using TesteDotkon.Domain.Interfaces;
using TesteDotkon.Infra.CrossCutting.NotificationPattern;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TesteDotkon.Infra.CrossCutting.Security.Autenticacao;

namespace TesteDotkon.Domain.Commands.Postagem.Deletar;

public class PostagemDeletarHandler(
    IUsuarioAutenticado usuarioAutenticado,
    IRepositoryPostagem repositoryPostagem,
    IValidator<PostagemDeletarRequest> validator)
    : Notifiable, IRequestHandler<PostagemDeletarRequest, CommandResponse<PostagemDeletarResponse>>
{
    public async Task<CommandResponse<PostagemDeletarResponse>> Handle(PostagemDeletarRequest request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            AddNotification("Validação", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return await Task.FromResult(new CommandResponse<PostagemDeletarResponse>(this));
        }

        var postagem = await repositoryPostagem.GetByAsync(false, p => p.Id == request.Id, cancellationToken);

        if (postagem is null)
        {
            AddNotification("Postagem", "Postagem Não Encontrada");
            return await Task.FromResult(new CommandResponse<PostagemDeletarResponse>(this));
        }

        if (postagem.AutorId != usuarioAutenticado.UsuarioId)
        {
            AddNotification("Postagem", "Postagem Não Pertence ao Usuário");
            return await Task.FromResult(new CommandResponse<PostagemDeletarResponse>(this));
        }

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<PostagemDeletarResponse>(this));

        repositoryPostagem.DeleteAsync(postagem);

        return await Task.FromResult(new CommandResponse<PostagemDeletarResponse>(new PostagemDeletarResponse(postagem.Id, "Postagem Deletada com sucesso"), this));
    }
}

using TesteDotkon.Infra.MediatoR.CommandResponse;
using TesteDotkon.Application.Interfaces;
using TesteDotkon.Infra.CrossCutting.IoC.interfaces;
using TesteDotkon.Infra.CrossCutting.NotificationPattern;
using TesteDotkon.Domain.Interfaces;
using TesteDotkon.Domain.Commands.Postagem.Adicionar;
using MediatR;
using TesteDotkon.Domain.Commands.Postagem.Deletar;
using TesteDotkon.Domain.Commands.Postagem.Editar;
using TesteDotkon.Domain.Commands.Postagem.Listar;

namespace TesteDotkon.Application.Services;

public class PostagemAppService(IUnitOfWork unitOfWork, ISender mediator)
    : Notifiable, IPostagemAppService, IInjectScoped
{
    public async Task<CommandResponse<PostagemAdicionarResponse>> Adicionar(PostagemAdicionarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<PostagemDeletarResponse>> Deletar(PostagemDeletarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<PostagemEditarResponse>> Editar(PostagemEditarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<PostagemListarResponse>> Listar(PostagemListarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        return commandResponse;
    }
}

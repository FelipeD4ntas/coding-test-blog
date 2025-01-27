using TesteDotkon.Infra.MediatoR.CommandResponse;
using TesteDotkon.Application.Interfaces;
using TesteDotkon.Infra.CrossCutting.IoC.interfaces;
using TesteDotkon.Infra.CrossCutting.NotificationPattern;
using TesteDotkon.Domain.Interfaces;
using TesteDotkon.Domain.Commands.Usuario.Adicionar;
using MediatR;

namespace TesteDotkon.Application.Services;

public class UsuarioAppService(IUnitOfWork unitOfWork, ISender mediator)
    : Notifiable, IUsuarioAppService, IInjectScoped
{
    public async Task<CommandResponse<UsuarioAdicionarResponse>> Adicionar(UsuarioAdicionarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }
}

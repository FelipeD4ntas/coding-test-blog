using TesteDotkon.Domain.Commands.Usuario.Adicionar;
using TesteDotkon.Infra.MediatoR.CommandResponse;

namespace TesteDotkon.Application.Interfaces;

public interface IUsuarioAppService
{
    Task<CommandResponse<UsuarioAdicionarResponse>> Adicionar(UsuarioAdicionarRequest request);
}

using TesteDotkon.Domain.Commands.Postagem.Adicionar;
using TesteDotkon.Domain.Commands.Postagem.Deletar;
using TesteDotkon.Domain.Commands.Postagem.Editar;
using TesteDotkon.Domain.Commands.Postagem.Listar;
using TesteDotkon.Infra.MediatoR.CommandResponse;

namespace TesteDotkon.Application.Interfaces;

public interface IPostagemAppService
{
    Task<CommandResponse<PostagemAdicionarResponse>> Adicionar(PostagemAdicionarRequest request);
    Task<CommandResponse<PostagemDeletarResponse>> Deletar(PostagemDeletarRequest request);
    Task<CommandResponse<PostagemEditarResponse>> Editar(PostagemEditarRequest request);
    Task<CommandResponse<PostagemListarResponse>> Listar(PostagemListarRequest request);
}

using MediatR;
using TesteDotkon.Infra.MediatoR.CommandResponse;
namespace TesteDotkon.Domain.Commands.Postagem.Adicionar;

public class PostagemAdicionarRequest : IRequest<CommandResponse<PostagemAdicionarResponse>>
{
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
}
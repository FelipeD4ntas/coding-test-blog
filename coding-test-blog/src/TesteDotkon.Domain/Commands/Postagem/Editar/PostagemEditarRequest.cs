using MediatR;
using TesteDotkon.Infra.MediatoR.CommandResponse;
namespace TesteDotkon.Domain.Commands.Postagem.Editar;

public class PostagemEditarRequest : IRequest<CommandResponse<PostagemEditarResponse>>
{
    public Guid PostagemId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
}
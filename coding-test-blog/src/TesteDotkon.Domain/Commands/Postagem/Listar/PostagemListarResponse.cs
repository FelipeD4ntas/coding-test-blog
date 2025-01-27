using TesteDotkon.Core.Domain.DTOs;
using TesteDotkon.Domain.DTOs;

namespace TesteDotkon.Domain.Commands.Postagem.Listar;

public class PostagemListarResponse(Guid id, string mensagem, List<PostagemDto> postagens) : CommandResponseDto(id, mensagem)
{
    public List<PostagemDto> Postagens { get; set; } = postagens;
}

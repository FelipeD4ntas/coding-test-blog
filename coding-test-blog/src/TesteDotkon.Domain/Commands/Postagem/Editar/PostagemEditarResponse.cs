using TesteDotkon.Core.Domain.DTOs;

namespace TesteDotkon.Domain.Commands.Postagem.Editar;

public class PostagemEditarResponse(Guid id, string mensagem) : CommandResponseDto(id, mensagem);

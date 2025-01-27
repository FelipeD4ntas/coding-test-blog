using TesteDotkon.Core.Domain.DTOs;

namespace TesteDotkon.Domain.Commands.Postagem.Deletar;

public class PostagemDeletarResponse(Guid id, string mensagem) : CommandResponseDto(id, mensagem);

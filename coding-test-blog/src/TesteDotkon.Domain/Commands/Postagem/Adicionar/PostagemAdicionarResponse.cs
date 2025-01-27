using TesteDotkon.Core.Domain.DTOs;

namespace TesteDotkon.Domain.Commands.Postagem.Adicionar;

public class PostagemAdicionarResponse(Guid id, string mensagem) : CommandResponseDto(id, mensagem);

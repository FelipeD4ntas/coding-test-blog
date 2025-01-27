using MediatR;
using TesteDotkon.Infra.MediatoR.CommandResponse;
namespace TesteDotkon.Domain.Commands.Postagem.Deletar;

public class PostagemDeletarRequest : IRequest<CommandResponse<PostagemDeletarResponse>>
{
    public Guid Id { get; set; }
}
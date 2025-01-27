using MediatR;
using TesteDotkon.Infra.MediatoR.CommandResponse;
namespace TesteDotkon.Domain.Commands.Postagem.Listar;

public class PostagemListarRequest : IRequest<CommandResponse<PostagemListarResponse>>;
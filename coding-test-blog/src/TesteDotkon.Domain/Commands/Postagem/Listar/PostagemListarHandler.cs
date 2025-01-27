using Mapster;
using MediatR;
using TesteDotkon.Infra.MediatoR.CommandResponse;
using TesteDotkon.Domain.Interfaces;
using TesteDotkon.Infra.CrossCutting.NotificationPattern;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TesteDotkon.Infra.CrossCutting.Security.Autenticacao;
using TesteDotkon.Domain.DTOs;

namespace TesteDotkon.Domain.Commands.Postagem.Listar;

public class PostagemListarHandler(
    IRepositoryUsuario repositoryUsuario,
    IRepositoryPostagem repositoryPostagem,
    IValidator<PostagemListarRequest> validator)
    : Notifiable, IRequestHandler<PostagemListarRequest, CommandResponse<PostagemListarResponse>>
{
    public async Task<CommandResponse<PostagemListarResponse>> Handle(PostagemListarRequest request, CancellationToken cancellationToken)
    {
        var postagens = await repositoryPostagem.ListAsync(false);

        if (postagens is null || !postagens.Any())
        {
            AddNotification("Postagem", "Nenhuma Postagem");
            return await Task.FromResult(new CommandResponse<PostagemListarResponse>(this));
        }

        var postagemTasks = postagens.Select(async postagem =>
        {
            var autor = await repositoryUsuario.GetByAsync(false, u => u.Id == postagem.AutorId, cancellationToken);
            return new PostagemDto
            {
                Id = postagem.Id,
                Titulo = postagem.Titulo,
                Conteudo = postagem.Conteudo,
                NomeAutor = autor?.Nome,
                AutorId = postagem.AutorId,
                DataPublicacao = postagem.DataPublicacao
            };
        });

        var postagemDtos = await Task.WhenAll(postagemTasks);

        var response = new PostagemListarResponse(
                           Guid.NewGuid(),
                           "Listagem de postagens realizada com sucesso",
                           postagemDtos.ToList());

        return await Task.FromResult(new CommandResponse<PostagemListarResponse>(response, this));
    }
}

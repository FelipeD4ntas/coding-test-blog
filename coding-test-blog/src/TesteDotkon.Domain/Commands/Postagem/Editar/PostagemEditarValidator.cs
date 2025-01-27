using FluentValidation;

namespace TesteDotkon.Domain.Commands.Postagem.Editar;

public class PostagemEditarValidator : AbstractValidator<PostagemEditarRequest>
{
    public PostagemEditarValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Titulo é obrigatório");

        RuleFor(x => x.Conteudo)
            .NotEmpty()
            .WithMessage("Conteúdo é obrigatório");
    }
}

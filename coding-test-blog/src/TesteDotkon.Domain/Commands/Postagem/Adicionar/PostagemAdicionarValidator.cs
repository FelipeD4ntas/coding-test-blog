using FluentValidation;

namespace TesteDotkon.Domain.Commands.Postagem.Adicionar;

public class PostagemAdicionarValidator : AbstractValidator<PostagemAdicionarRequest>
{
    public PostagemAdicionarValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Titulo é obrigatório");

        RuleFor(x => x.Conteudo)
            .NotEmpty()
            .WithMessage("Conteúdo é obrigatório");
    }
}

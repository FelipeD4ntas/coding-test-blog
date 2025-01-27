using FluentValidation;

namespace TesteDotkon.Domain.Commands.Postagem.Deletar;

public class PostagemDeletarValidator : AbstractValidator<PostagemDeletarRequest>
{
    public PostagemDeletarValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id postagem é obrigatório");
    }
}

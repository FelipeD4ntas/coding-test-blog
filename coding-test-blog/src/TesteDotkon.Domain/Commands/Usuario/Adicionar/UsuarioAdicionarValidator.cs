using FluentValidation;

namespace TesteDotkon.Domain.Commands.Usuario.Adicionar;

public class UsuarioAdicionarValidator : AbstractValidator<UsuarioAdicionarRequest>
{
    public UsuarioAdicionarValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório");

        RuleFor(x => x.Senha)
            .NotEmpty()
            .WithMessage("Senha é obrigatória");

        RuleFor(x => x.ConfirmacaoSenha)
            .NotEmpty()
            .WithMessage("Confirmação de senha é obrigatória")
            .Equal(x => x.Senha)
            .WithMessage("As senhas não conferem");
    }
}

using TesteDotkon.Core.Domain.Entities.Base;

namespace TesteDotkon.Domain.Entities;

public class Postagem : EntidadeBase
{
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public Guid AutorId { get; set; }
    public DateTime DataPublicacao { get; set; } = DateTime.Now;
    public virtual Usuario? Autor { get; set; }

    public void Editar(string titulo, string conteudo)
    {
        Titulo = titulo;
        Conteudo = conteudo;
    }
}

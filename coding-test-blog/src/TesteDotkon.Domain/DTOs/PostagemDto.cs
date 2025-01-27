namespace TesteDotkon.Domain.DTOs;

public class PostagemDto
{
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Conteudo { get; set; } 
    public string? NomeAutor { get; set; }
    public Guid AutorId { get; set; }
    public DateTime DataPublicacao { get; set; }
}

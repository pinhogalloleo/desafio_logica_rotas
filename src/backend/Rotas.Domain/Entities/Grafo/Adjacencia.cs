
namespace Rotas.Domain.Entities.Grafo;

public record Adjacencia
{
    public string Destino { get; set; }
    public decimal Custo { get; set; }

    public Adjacencia(string destino, decimal custo)
    {
        Destino = destino;
        Custo = custo;
    }
}

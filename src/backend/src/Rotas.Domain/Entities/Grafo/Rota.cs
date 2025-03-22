
namespace Rotas.Domain.Entities.Grafo;

public class Rota
{
    public List<string> Caminho { get; set; }
    public decimal CustoTotal { get; set; }

    public Rota()
    {
        Caminho = new List<string>();
        CustoTotal = 0;
    }

    public Rota(List<string> caminho, decimal custoTotal)
    {
        Caminho = caminho;
        CustoTotal = custoTotal;
    }
}

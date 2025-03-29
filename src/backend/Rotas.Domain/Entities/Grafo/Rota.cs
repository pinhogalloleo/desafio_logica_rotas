namespace Rotas.Domain.Entities.Grafo;

public class Rota
{
    public IEnumerable<Deslocamento> Caminhos { get; set; }
    public decimal CustoTotal { get; set; }

    public Rota()
    {
        Caminhos = new List<Deslocamento>();
        CustoTotal = 0;
    }

    public Rota(IEnumerable<Deslocamento> caminhos, decimal custoTotal)
    {
        Caminhos = caminhos;
        CustoTotal = custoTotal;
    }
}

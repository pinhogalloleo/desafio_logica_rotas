
namespace Rotas.Domain.Entities.Grafo;
public class Grafo
{
    public Dictionary<string, List<Adjacencia>> Adjacencias { get; private set; }

    public Grafo(List<Deslocamento> viagens)
    {
        if (viagens == null)
            throw new ArgumentNullException(nameof(viagens));
            
        Adjacencias = new Dictionary<string, List<Adjacencia>>();

        foreach (var deslocamento in viagens)
        {
            if (!Adjacencias.ContainsKey(deslocamento.Origem))
                Adjacencias[deslocamento.Origem] = new List<Adjacencia>();

            Adjacencias[deslocamento.Origem].Add(new Adjacencia(deslocamento.Destino, deslocamento.Custo));

            // Ensure the destination is also added as a key, even if it has no outgoing edges
            if (!Adjacencias.ContainsKey(deslocamento.Destino))
                Adjacencias[deslocamento.Destino] = new List<Adjacencia>();
        }
    }

    public List<Adjacencia> ObterAdjacencias(string origem)
    {
        if (Adjacencias.ContainsKey(origem))
            return Adjacencias[origem];

        return new List<Adjacencia>();
    }
}

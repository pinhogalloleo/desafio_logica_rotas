
namespace Rotas.Domain.Entities.Grafo;
public class Grafo
{
    public Dictionary<string, List<Adjacencia>> Adjacencias { get; private set; }

    public Grafo(List<Viagem> viagens)
    {
        if (viagens == null)
            throw new ArgumentNullException(nameof(viagens));
            
        Adjacencias = new Dictionary<string, List<Adjacencia>>();

        foreach (var viagem in viagens)
        {
            if (!Adjacencias.ContainsKey(viagem.Origem))
                Adjacencias[viagem.Origem] = new List<Adjacencia>();

            Adjacencias[viagem.Origem].Add(new Adjacencia(viagem.Destino, viagem.Custo));

            // Ensure the destination is also added as a key, even if it has no outgoing edges
            if (!Adjacencias.ContainsKey(viagem.Destino))
                Adjacencias[viagem.Destino] = new List<Adjacencia>();
        }
    }

    public List<Adjacencia> ObterAdjacencias(string origem)
    {
        if (Adjacencias.ContainsKey(origem))
            return Adjacencias[origem];

        return new List<Adjacencia>();
    }
}

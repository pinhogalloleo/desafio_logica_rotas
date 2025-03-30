using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Entities.Grafo;


namespace Rotas.Domain.Services;

public class CalculoRotaService(IRepositoryCrud<Deslocamento> repository) : ICalculoRotaService
{
    private readonly IRepositoryCrud<Deslocamento> _repository = repository;

    /// <summary>
    /// Localiza a melhor rota entre dois pontos com lógica parecida com Grafo
    /// </summary>
    public async Task<Rota?> CalcularRotaAsync(string origem, string destino)
    {
        var viagens = await _repository.GetAllAsync();
        if (viagens == null || !viagens.Any())
            throw new NaoEncontradoException("Nenhum deslocamento encontrado para calcular a rota.");

        var grafo = new Grafo(viagens);
        var rota = EncontrarMelhorRota(grafo, origem, destino, viagens);
        return rota;
    }

    private static Rota? EncontrarMelhorRota(Grafo grafo, string origem, string destino, IEnumerable<Deslocamento> deslocamentos)
    {
        var distancias = new Dictionary<string, decimal>(StringComparer.Ordinal);
        var predecessores = new Dictionary<string, string>(StringComparer.Ordinal);
        var filaPrioridade = new SortedSet<(decimal Custo, string Vertice)>();

        foreach (var verticeIn in grafo.Adjacencias.Keys)
            distancias[verticeIn] = decimal.MaxValue;

        distancias[origem] = 0;
        filaPrioridade.Add((0, origem));

        while (filaPrioridade.Any())
        {
            var (custoAtual, verticeAtual) = filaPrioridade.Min;
            filaPrioridade.Remove(filaPrioridade.Min);

            if (custoAtual > distancias[verticeAtual])
                continue;

            foreach (var adjacencia in grafo.ObterAdjacencias(verticeAtual))
            {
                var custoNovo = custoAtual + adjacencia.Custo;

                if (custoNovo < distancias[adjacencia.Destino])
                {
                    distancias[adjacencia.Destino] = custoNovo;
                    predecessores[adjacencia.Destino] = verticeAtual;
                    filaPrioridade.Add((custoNovo, adjacencia.Destino));
                }
            }
        }

        if (!distancias.TryGetValue(destino, out var distanciaDestino) || distanciaDestino == decimal.MaxValue)
            return null; // Rota não encontrada

        var caminho = new List<Deslocamento>();
        var vertice = destino;

        while (vertice != origem)
        {
            if (!predecessores.ContainsKey(vertice))
                throw new KeyNotFoundException($"The given key '{vertice}' was not present in the dictionary.");

            var origemVertice = predecessores[vertice];
            var deslocamento = deslocamentos.FirstOrDefault(d => d.Origem == origemVertice && d.Destino == vertice);

            if (deslocamento == null)
                throw new InvalidOperationException($"No displacement found from {origemVertice} to {vertice}.");

            caminho.Add(deslocamento);
            vertice = origemVertice;
        }

        caminho.Reverse();
        var distanciasFinal = distancias[destino];
        var caminhoFinal = caminho;
        var rota = new Rota(caminho, distancias[destino]);

        return rota;
    }


}//..class

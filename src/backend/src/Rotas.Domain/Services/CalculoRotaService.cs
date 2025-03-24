
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
        if (viagens == null || viagens.Count == 0)
            throw new NaoEncontradoException("Nenhuma deslocamento encontrada");

        var grafo = new Grafo(viagens);
        var rota = EncontrarMelhorRota(grafo, origem, destino);
        return rota;
    }

    private static Rota? EncontrarMelhorRota(Grafo grafo, string origem, string destino)
    {
        var distancias = new Dictionary<string, decimal>();
        var predecessores = new Dictionary<string, string>();
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

        if (!distancias.ContainsKey(destino) || distancias[destino] == decimal.MaxValue)
            return null; // Rota não encontrada

        var caminho = new List<string>();
        var vertice = destino;

        while (vertice != origem)
        {
            if (!predecessores.ContainsKey(vertice))
                throw new KeyNotFoundException($"The given key '{vertice}' was not present in the dictionary.");

            caminho.Add(vertice);
            vertice = predecessores[vertice];
        }
        caminho.Add(origem);
        caminho.Reverse();

        return new Rota(caminho, distancias[destino]);
    }


}//..class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Rotas.Domain.Services;

public class CalculoRotaService(IRepositoryCrud<Viagem> repository) : ICalculoRotaService
{
    private readonly IRepositoryCrud<Viagem> _repository = repository;

    /// <summary>
    /// Localiza a melhor rota entre dois pontos com lógica parecida com Grafo
    /// </summary>
    public async Task<List<Viagem>> CalcularRotaAsync(string origem, string destino)
    {
        var viagens = await _repository.GetAllAsync();
        if (viagens == null || viagens.Count == 0)
            throw new NaoEncontradoException("Nenhuma viagem encontrada");

        Dictionary<string, List<Viagem>> graph = MountGraph(viagens);

        List<Viagem> melhorRota = CalculateBestRoute(graph, origem, destino);
        
        return melhorRota;
    }

    private static List<Viagem> CalculateBestRoute(Dictionary<string, List<Viagem>> graph, string origem, string destino)
    {
        var listaMelhorRota = new List<Viagem>();
        // TODO TO DO: implement graph algorithm

        return listaMelhorRota;
    }


    private static Dictionary<string, List<Viagem>> MountGraph(List<Viagem> viagens)
    {
        var graph = new Dictionary<string, List<Viagem>>();
        foreach (var viagem in viagens)
        {
            if (!graph.ContainsKey(viagem.Origem))
                graph[viagem.Origem] = new();

            graph[viagem.Origem].Add(viagem);
        }

        return graph;
    }


}//..class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Rotas.Domain.Services
{
    public class CalculoRotaService(IRepositoryCrud<Viagem> repository)
    {
        private readonly IRepositoryCrud<Viagem> _repository = repository;

        /// <summary>
        /// Localiza a melhor rota entre dois pontos com lógica parecida com Grafo
        /// </summary>
        public async Task<List<Viagem>> LocalizarMelhorRota(string origem, string destino)
        {
            var viagens = await _repository.GetAllAsync();
            if (viagens == null || viagens.Count == 0)
                throw new NaoEncontradoException("Nenhuma viagem encontrada");

            // logica de grafo, por hora devolver a lista de viagens
            // foreach (var viagem in viagens) {...}

            return viagens;
        }


    }
}
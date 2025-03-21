using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Application.Dtos;
using Rotas.Domain.Services;
using Rotas.Domain.Entities;

namespace Rotas.Application.UseCases.Viagens
{
    public class AddViagemUseCase(CadastroViagemService cadastroViagemService)
    {
        private readonly CadastroViagemService _cadastroViagemService = cadastroViagemService;

        public async Task<int> ExecuteAsync(ViagemDto viagemDto)
        {
            var viagem = new Viagem()
            {
                Origem = viagemDto.Origem,
                Destino = viagemDto.Destino,
                Custo = viagemDto.Custo
            };

            return await _cadastroViagemService.InsertViagemAsync(viagem);
        }

    }
}
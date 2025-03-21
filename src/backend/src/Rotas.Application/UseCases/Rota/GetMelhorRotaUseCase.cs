using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Application.Dtos;
using Rotas.Domain.Services;

namespace Rotas.Application.UseCases.Rotas
{
    public class GetMelhorRotaUseCase
    {
        private readonly CalculoRotaService calculoRotaService;
        public GetMelhorRotaUseCase(CalculoRotaService calculoRotaService)
        {
            this.calculoRotaService = calculoRotaService;
        }

        public async Task<RotaResponseDto> Execute(string origem, string destino)
        {
            var rota = new RotaResponseDto();
            var listaViagens = await calculoRotaService.LocalizarMelhorRota(origem, destino);
            rota.ListaViagens = listaViagens;
            rota.Origem = origem;
            rota.Destino = destino;

            return rota;
        }


    } //..class
} //..namespace
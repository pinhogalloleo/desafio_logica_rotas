using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Application.Dtos;
using Rotas.Domain.Services;
using Rotas.Application.UseCases;

namespace Rotas.Application.UseCases.Rota.CalculoRota;

public class CalculoRotaUseCase(CalculoRotaService calculoRotaService):BaseUseCase
{
    private readonly CalculoRotaService _calculoRotaService = calculoRotaService;

    public async Task<RotaResponseDto> ExecuteAsync(string origem, string destino)
    {
        var rota = new RotaResponseDto();
        var listaViagens = await _calculoRotaService.CalcularRotaAsync(origem, destino);
        rota.ListaViagens = listaViagens;
        rota.Origem = origem;
        rota.Destino = destino;

        return rota;
    }


} //..class


using Rotas.Application.UseCases;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Domain.Entities.Grafo;

namespace Rotas.Application.Services;

public class CalculoMelhorRotaService(IUseCase<CalculoRotaDto, Task<Rota?>> calculoRotaUseCase) : ICalculoMelhorRotaService
{
    private readonly IUseCase<CalculoRotaDto, Task<Rota?>> calculoRotaUseCase = calculoRotaUseCase;

    public async Task<Rota?> CalcularMelhorRotaAsync(CalculoRotaDto dto) => await calculoRotaUseCase.ExecuteAsync(dto);
}

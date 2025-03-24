
using Rotas.Domain.Entities.Grafo;
using Rotas.Domain.Interfaces;

namespace Rotas.Application.UseCases.CalculoRota;

public class CalculoRotaUseCase(ICalculoRotaService calculoRotaService) : BaseUseCase<CalculoRotaDto>, IUseCase<CalculoRotaDto, Task<Rota?>>
{
    private readonly ICalculoRotaService _calculoRotaService = calculoRotaService;

    public async Task<Rota?> ExecuteAsync(CalculoRotaDto dto)
    {
        ValidateDto(dto);

        var rota = await _calculoRotaService.CalcularRotaAsync(dto.Origem, dto.Destino);
        return rota;
    }


} //..class

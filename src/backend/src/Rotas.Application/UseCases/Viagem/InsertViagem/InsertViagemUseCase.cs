
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace Rotas.Application.UseCases.Viagens.Insert;

public class InsertViagemUseCase(IRepositoryCrud<Viagem> repository, IViagemValidationService viagemValidationService) : BaseUseCase
{
    private readonly IRepositoryCrud<Viagem> _repository = repository;
    private readonly IViagemValidationService _viagemValidationService = viagemValidationService;

    public async Task<int> ExecuteAsync(InsertViagemDto viagemDto)
    {
        ValidateDto(viagemDto);

        await _viagemValidationService.ValidateDuplicityAsync(viagemDto.Origem, viagemDto.Destino);

        var viagem = new Viagem()
        {
            Origem = viagemDto.Origem.Trim().ToUpperInvariant(),
            Destino = viagemDto.Destino.Trim().ToUpperInvariant(),
            Custo = viagemDto.Custo
        };
        return await _repository.InsertAsync(viagem);
    }

}//..class

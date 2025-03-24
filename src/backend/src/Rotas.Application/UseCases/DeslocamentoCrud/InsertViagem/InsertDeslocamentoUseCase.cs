
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace Rotas.Application.UseCases.DeslocamentoCrud.Insert;

public class InsertDeslocamentoUseCase : BaseUseCase<InsertDeslocamentoDto>, IUseCase<InsertDeslocamentoDto, Task<int>>
{
    private readonly IRepositoryCrud<Deslocamento> _repository;
    private readonly IDeslocamentoDuplicityValidationService _deslocamentoValidationService;

    public InsertDeslocamentoUseCase(IRepositoryCrud<Deslocamento> repository, IDeslocamentoDuplicityValidationService deslocamentoValidationService)
    {
        this._repository = repository;
        this._deslocamentoValidationService = deslocamentoValidationService;
    }

    public async Task<int> ExecuteAsync(InsertDeslocamentoDto deslocamentoDto)
    {
        ValidateDto(deslocamentoDto);

        await _deslocamentoValidationService.ValidateDuplicityAsync(deslocamentoDto.Origem, deslocamentoDto.Destino);

        var deslocamento = new Deslocamento()
        {
            Origem = deslocamentoDto.Origem.Trim().ToUpperInvariant(),
            Destino = deslocamentoDto.Destino.Trim().ToUpperInvariant(),
            Custo = deslocamentoDto.Custo
        };
        return await _repository.InsertAsync(deslocamento);
    }

}//..class

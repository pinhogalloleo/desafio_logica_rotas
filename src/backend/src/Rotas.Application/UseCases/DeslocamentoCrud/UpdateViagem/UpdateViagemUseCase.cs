
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace Rotas.Application.UseCases.DeslocamentoCrud.Update;

public class UpdateDeslocamentoUseCase(IRepositoryCrud<Deslocamento> repository, IDeslocamentoDuplicityValidationService deslocamentoValidationService) 
: BaseUseCase<UpdateDeslocamentoDto>, IUseCase<UpdateDeslocamentoDto, Task>
{
    private readonly IRepositoryCrud<Deslocamento> _repository = repository;
    private readonly IDeslocamentoDuplicityValidationService _deslocamentoValidationService = deslocamentoValidationService;

    public async Task ExecuteAsync(UpdateDeslocamentoDto deslocamentoDto)
    {
        ValidateDto(deslocamentoDto);

        var deslocamentoExistente = await _repository.GetByIdAsync(deslocamentoDto.Id);
        if (deslocamentoExistente == null)
            throw new NaoEncontradoException($"Deslocamento não encontrada pelo Id [{deslocamentoDto.Id}]");

        await _deslocamentoValidationService.ValidateDuplicityAsync(deslocamentoDto.Origem, deslocamentoDto.Destino, deslocamentoDto.Id);

        deslocamentoExistente.Origem = deslocamentoDto.Origem;
        deslocamentoExistente.Destino = deslocamentoDto.Destino;
        deslocamentoExistente.Custo = deslocamentoDto.Custo;

        await _repository.UpdateAsync(deslocamentoExistente);
    }


}//..class


using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Rotas.Application.UseCases.DeslocamentoCrud.GetById;

public class GetByIdDeslocamentoUseCase(IRepositoryCrud<Deslocamento> repository)
    : BaseUseCase<GetByIdDeslocamentoDto>, IUseCase<GetByIdDeslocamentoDto, Task<Deslocamento>>
{
    private readonly IRepositoryCrud<Deslocamento> _repository = repository;

    public async Task<Deslocamento> ExecuteAsync(GetByIdDeslocamentoDto dto)
    {
        ValidateDto(dto);

        var deslocamentoExistente = await _repository.GetByIdAsync(dto.Id);
        if (deslocamentoExistente == null)
            throw new NaoEncontradoException($"Deslocamento não encontrada pelo Id [{dto.Id}]");

        return deslocamentoExistente;
    }


}//..class

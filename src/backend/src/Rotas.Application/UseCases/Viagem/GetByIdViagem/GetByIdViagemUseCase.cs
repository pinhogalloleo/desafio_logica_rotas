
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Rotas.Application.UseCases.Viagens.GetById;

public class GetByIdViagemUseCase(IRepositoryCrud<Viagem> repository) : BaseUseCase
{
    private readonly IRepositoryCrud<Viagem> _repository = repository;

    public async Task<Viagem> ExecuteAsync(GetByIdViagemDto dto)
    {
        ValidateDto(dto);

        var viagemExistente = await _repository.GetByIdAsync(dto.Id);
        if (viagemExistente == null)
            throw new NaoEncontradoException($"Viagem não encontrada pelo Id [{dto.Id}]");
       
        return viagemExistente;
    }


}//..class

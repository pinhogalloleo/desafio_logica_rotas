using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Rotas.Application.UseCases.Viagens.Delete;

public class DeleteViagemUseCase(IRepositoryCrud<Viagem> repository):BaseUseCase
{
    private readonly IRepositoryCrud<Viagem> _repository = repository;

    public async Task ExecuteAsync(DeleteViagemDto viagemDto)
    {
        ValidateDto(viagemDto);

        var viagemExistente = await _repository.GetByIdAsync(viagemDto.Id);
        if (viagemExistente == null)
            throw new NaoEncontradoException($"Viagem não encontrada pelo Id [{viagemDto.Id}]");

        await _repository.DeleteAsync(viagemDto.Id);
    }
}

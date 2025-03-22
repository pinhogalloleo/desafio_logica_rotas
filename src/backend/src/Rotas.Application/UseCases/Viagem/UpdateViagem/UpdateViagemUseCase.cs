using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Application.Dtos;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace Rotas.Application.UseCases.Viagens.Update;

public class UpdateViagemUseCase(IRepositoryCrud<Viagem> repository, ViagemValidationService viagemValidationService) : BaseUseCase
{
    private readonly IRepositoryCrud<Viagem> _repository = repository;
    private readonly ViagemValidationService _viagemValidationService = viagemValidationService;

    public async Task ExecuteAsync(UpdateViagemDto viagemDto)
    {
        ValidateDto(viagemDto);

        var viagemExistente = await _repository.GetByIdAsync(viagemDto.Id);
        if (viagemExistente == null)
            throw new NaoEncontradoException($"Viagem não encontrada pelo Id [{viagemDto.Id}]");

        await _viagemValidationService.ValidateDuplicityAsync(viagemDto.Origem, viagemDto.Destino, viagemDto.Id);

        viagemExistente.Origem = viagemDto.Origem;
        viagemExistente.Destino = viagemDto.Destino;
        viagemExistente.Custo = viagemDto.Custo;

        await _repository.UpdateAsync(viagemExistente);
    }


}//..class


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

namespace Rotas.Application.UseCases.Viagens.Insert;

public class InsertViagemUseCase(IRepositoryCrud<Viagem> repository, ViagemValidationService viagemValidationService) : BaseUseCase
{
    private readonly IRepositoryCrud<Viagem> _repository = repository;
    private readonly ViagemValidationService _viagemValidationService = viagemValidationService;

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

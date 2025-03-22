using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Entities;
using Rotas.Application.UseCases.Viagens.Insert;
using Rotas.Application.UseCases.Viagens.Update;
using Rotas.Application.UseCases.Viagens.Delete;
using Rotas.Application.UseCases.Viagens.GetById;
using Rotas.Application.UseCases.Viagens.GetAll;

namespace Rotas.Application.Services;

public class ViagemService
{
    private readonly InsertViagemUseCase _insertViagemUseCase;
    private readonly UpdateViagemUseCase _updateViagemUseCase;
    private readonly DeleteViagemUseCase _deleteViagemUseCase;
    private readonly GetByIdViagemUseCase _getByIdViagemUseCase;
    private readonly GetAllViagemUseCase _getAllViagemUseCase;

    public ViagemService(
        InsertViagemUseCase insertViagemUseCase,
        UpdateViagemUseCase updateViagemUseCase,
        DeleteViagemUseCase deleteViagemUseCase,
        GetByIdViagemUseCase getByIdViagemUseCase,
        GetAllViagemUseCase getAllViagemUseCase)
    {
        _insertViagemUseCase = insertViagemUseCase;
        _updateViagemUseCase = updateViagemUseCase;
        _deleteViagemUseCase = deleteViagemUseCase;
        _getByIdViagemUseCase = getByIdViagemUseCase;
        _getAllViagemUseCase = getAllViagemUseCase;
    }

    public async Task<int> InsertViagemAsync(InsertViagemDto dto) =>
        await _insertViagemUseCase.ExecuteAsync(dto);

    public async Task UpdateViagemAsync(UpdateViagemDto dto) =>
        await _updateViagemUseCase.ExecuteAsync(dto);

    public async Task DeleteViagemAsync(DeleteViagemDto dto) =>
        await _deleteViagemUseCase.ExecuteAsync(dto);

    public async Task<Viagem> GetByIdAsync(GetByIdViagemDto dto) =>
        await _getByIdViagemUseCase.ExecuteAsync(dto);

    public async Task<List<Viagem>> GetAllAsync() =>
        await _getAllViagemUseCase.ExecuteAsync();
}

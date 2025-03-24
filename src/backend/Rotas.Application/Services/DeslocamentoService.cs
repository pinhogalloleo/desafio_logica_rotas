
using Rotas.Domain.Entities;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;
using Rotas.Application.UseCases.DeslocamentoCrud.GetAll;
using Rotas.Application.UseCases;

namespace Rotas.Application.Services;

public class DeslocamentoService : IDeslocamentoService
{
    // refer to IUseCase for _insertDeslocamentoUseCase
    private readonly IUseCase<InsertDeslocamentoDto, Task<int>> _insertDeslocamentoUseCase;
    private readonly IUseCase<UpdateDeslocamentoDto, Task> _updateDeslocamentoUseCase;
    private readonly IUseCase<DeleteDeslocamentoDto, Task> _deleteDeslocamentoUseCase;
    private readonly IUseCase<GetByIdDeslocamentoDto, Task<Deslocamento>> _getByIdDeslocamentoUseCase;
    private readonly IUseCase<Task<List<Deslocamento>>> _getAllDeslocamentoUseCase;

    public DeslocamentoService(
        IUseCase<InsertDeslocamentoDto, Task<int>> insertDeslocamentoUseCase,
        IUseCase<UpdateDeslocamentoDto, Task> updateDeslocamentoUseCase,
        IUseCase<DeleteDeslocamentoDto, Task> deleteDeslocamentoUseCase,
        IUseCase<GetByIdDeslocamentoDto, Task<Deslocamento>> getByIdDeslocamentoUseCase,
        IUseCase<Task<List<Deslocamento>>> getAllDeslocamentoUseCase)
    {
        _insertDeslocamentoUseCase = insertDeslocamentoUseCase;
        _updateDeslocamentoUseCase = updateDeslocamentoUseCase;
        _deleteDeslocamentoUseCase = deleteDeslocamentoUseCase;
        _getByIdDeslocamentoUseCase = getByIdDeslocamentoUseCase;
        _getAllDeslocamentoUseCase = getAllDeslocamentoUseCase;
    }

    public async Task<int> InsertDeslocamentoAsync(InsertDeslocamentoDto dto) =>
        await this._insertDeslocamentoUseCase.ExecuteAsync(dto);

    public async Task UpdateDeslocamentoAsync(UpdateDeslocamentoDto dto) =>
        await this._updateDeslocamentoUseCase.ExecuteAsync(dto);

    public async Task DeleteDeslocamentoAsync(DeleteDeslocamentoDto dto) =>
        await this._deleteDeslocamentoUseCase.ExecuteAsync(dto);

    public async Task<Deslocamento> GetByIdAsync(GetByIdDeslocamentoDto dto) =>
        await this._getByIdDeslocamentoUseCase.ExecuteAsync(dto);

    public async Task<List<Deslocamento>> GetAllAsync() =>
        await this._getAllDeslocamentoUseCase.ExecuteAsync();
}

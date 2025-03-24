
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;

namespace Rotas.Application.UseCases.DeslocamentoCrud.GetAll;

public class GetAllDeslocamentoUseCase(IRepositoryCrud<Deslocamento> repository) : IUseCase<Task<List<Deslocamento>>>
{
    private readonly IRepositoryCrud<Deslocamento> _repository = repository;

    public async Task<List<Deslocamento>> ExecuteAsync() => await _repository.GetAllAsync();

}


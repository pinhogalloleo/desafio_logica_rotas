
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;

namespace Rotas.Application.UseCases.DeslocamentoCrud.GetAll;

public class GetAllDeslocamentoUseCase(IRepositoryCrud<Deslocamento> repository) : IUseCase<Task<IEnumerable<Deslocamento>>>
{
    private readonly IRepositoryCrud<Deslocamento> _repository = repository;

    public async Task<IEnumerable<Deslocamento>> ExecuteAsync() => await _repository.GetAllAsync();

}


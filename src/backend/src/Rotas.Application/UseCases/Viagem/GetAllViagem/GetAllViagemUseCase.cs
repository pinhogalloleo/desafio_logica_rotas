
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;

namespace Rotas.Application.UseCases.Viagens.GetAll;

public class GetAllViagemUseCase(IRepositoryCrud<Viagem> repository)
{
    private readonly IRepositoryCrud<Viagem> _repository = repository;
    
    public async Task<List<Viagem>> ExecuteAsync() => await _repository.GetAllAsync();
    
}


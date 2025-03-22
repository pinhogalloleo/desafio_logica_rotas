
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Rotas.Domain.Services;

public class ViagemValidationService(IRepositoryCrud<Viagem> repository) : IViagemValidationService
{
    private readonly IRepositoryCrud<Viagem> _repository = repository;

    public async Task ValidateDuplicityAsync(string origem, string destino, int? excludeId = null)
    {
        var cleanOrigem = origem.Trim().ToUpperInvariant();
        var cleanDestino = destino.Trim().ToUpperInvariant();

        var list = await _repository.SearchByExpressionAsync(x =>
            string.Equals(x.Origem, cleanOrigem, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(x.Destino, cleanDestino, StringComparison.OrdinalIgnoreCase) &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (list == null || list.Count == 0)
            return;

        throw new DuplicidadeException($"Já existe outra Viagem cadastrada com origem {origem} e destino {destino}");
    }

}

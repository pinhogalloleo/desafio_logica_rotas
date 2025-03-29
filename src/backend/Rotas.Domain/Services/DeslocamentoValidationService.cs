
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Rotas.Domain.Services;

public class DeslocamentoValidationService(IRepositoryCrud<Deslocamento> repository) : IDeslocamentoDuplicityValidationService
{
    private readonly IRepositoryCrud<Deslocamento> _repository = repository;

    public async Task ValidateDuplicityAsync(string origem, string destino, int? excludeId = null)
    {
        var cleanOrigem = origem.Trim().ToUpperInvariant();
        var cleanDestino = destino.Trim().ToUpperInvariant();

        var list = await this._repository.SearchByExpressionAsync(x =>
            (x.Origem == cleanOrigem && x.Destino == cleanDestino)
            &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (list == null || list.Count == 0)
            return;

        throw new DuplicidadeException($"Já existe outra Deslocamento cadastrada com origem {origem} e destino {destino}");
    }

}

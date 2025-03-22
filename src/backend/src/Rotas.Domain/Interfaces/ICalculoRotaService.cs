
using Rotas.Domain.Entities.Grafo;

namespace Rotas.Domain.Interfaces;
public interface ICalculoRotaService
{
    Task<Rota?> CalcularRotaAsync(string origem, string destino);
}

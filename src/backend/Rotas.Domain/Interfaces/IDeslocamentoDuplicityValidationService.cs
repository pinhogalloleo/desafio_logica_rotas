
namespace Rotas.Domain.Interfaces;
public interface IDeslocamentoDuplicityValidationService
{
    Task ValidateDuplicityAsync(string origem, string destino, int? excludeId = null);
}

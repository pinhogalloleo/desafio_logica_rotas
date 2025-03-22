using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rotas.Domain.Interfaces;
public interface IViagemValidationService
{
    Task ValidateDuplicityAsync(string origem, string destino, int? excludeId = null);
}

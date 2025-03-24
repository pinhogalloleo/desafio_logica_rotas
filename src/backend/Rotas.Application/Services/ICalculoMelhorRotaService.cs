using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Domain.Entities.Grafo;

namespace Rotas.Application.Services
{
    public interface ICalculoMelhorRotaService
    {
        Task<Rota?> CalcularMelhorRotaAsync(CalculoRotaDto dto);
    }
}
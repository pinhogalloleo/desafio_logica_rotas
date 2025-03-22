using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Entities;

namespace Rotas.Domain.Interfaces
{
    public interface ICalculoRotaService
    {
        Task<List<Viagem>> CalcularRotaAsync(string origem, string destino);
    }
}
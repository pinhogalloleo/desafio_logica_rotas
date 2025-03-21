
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Entities;

namespace Rotas.Application.Dtos
{
    public class RotaResponseDto
    {
        public List<Viagem> ListaViagens { get; set; }
        public string? Origem { get; set; }
        public string? Destino { get; set; }
        public decimal? CustoTotal
        {
            get
            {
                if (ListaViagens == null || ListaViagens.Count == 0)
                    return null;

                return ListaViagens.Sum(v => v.Custo);
            }
        }

        public RotaResponseDto()
        {
            ListaViagens = new List<Viagem>();
        }

    }
}

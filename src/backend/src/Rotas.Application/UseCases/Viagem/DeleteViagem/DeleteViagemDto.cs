using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rotas.Application.UseCases.Viagens.Delete;

public class DeleteViagemDto
{
    [Required(ErrorMessage = "Id é obrigatório para exclusão")]
    [Range(1,9999, ErrorMessage = "Valor mínimo para Id é 1 e máximo 9999")]
    public required int Id {get;set;}
}

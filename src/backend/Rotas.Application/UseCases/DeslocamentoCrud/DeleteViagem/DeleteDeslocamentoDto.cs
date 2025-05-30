
using System.ComponentModel.DataAnnotations;

namespace Rotas.Application.UseCases.DeslocamentoCrud.Delete;

public class DeleteDeslocamentoDto
{
    [Required(ErrorMessage = "Id é obrigatório para exclusão")]
    [Range(1,9999, ErrorMessage = "Valor mínimo para Id é 1 e máximo 9999")]
    public required int Id {get;set;}
}

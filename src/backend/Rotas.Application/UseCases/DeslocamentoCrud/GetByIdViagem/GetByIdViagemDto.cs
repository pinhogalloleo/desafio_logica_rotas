
using System.ComponentModel.DataAnnotations;

namespace Rotas.Application.UseCases.DeslocamentoCrud.GetById;

public class GetByIdDeslocamentoDto
{
    [Required(ErrorMessage = "Id é obrigatório")]
    public int Id { get; set; }
}

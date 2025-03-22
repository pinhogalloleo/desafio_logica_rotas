using System.ComponentModel.DataAnnotations;

namespace Rotas.Application.UseCases.Viagens.GetById;

public class GetByIdViagemDto
{
    [Required(ErrorMessage = "Id é obrigatório")]
    public int Id { get; set; }
}

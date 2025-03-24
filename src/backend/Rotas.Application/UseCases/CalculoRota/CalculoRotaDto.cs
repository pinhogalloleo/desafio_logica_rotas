
using System.ComponentModel.DataAnnotations;

namespace Rotas.Application.UseCases.CalculoRota;

public class CalculoRotaDto
{
    [MinLength(3, ErrorMessage = "Origem deve ter pelo menos 3 letras")]
    [MaxLength(15, ErrorMessage = "Origem deve ter no máximo 15 letras")]
    [Required(ErrorMessage = "Origem é obrigatório")]
    public required string Origem { get; set; }

    [MinLength(3, ErrorMessage = "Destino deve ter pelo menos 3 letras")]
    [MaxLength(15, ErrorMessage = "Destino deve ter no máximo 15 letras")]
    [Required(ErrorMessage = "Destino é obrigatório")]
    public required string Destino { get; set; }
}

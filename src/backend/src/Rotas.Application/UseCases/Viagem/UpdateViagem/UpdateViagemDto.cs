using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rotas.Application.UseCases.Viagens.Update;
public class UpdateViagemDto
{
    [Required(ErrorMessage = "Id é obrigatório para atualização")]
    [Range(1,9999, ErrorMessage = "Valor mínimo para Id é 1 e máximo 9999")]
    public required int Id {get;set;}

    [MinLength(3, ErrorMessage = "Origem deve ter pelo menos 3 letras")]
    [MaxLength(15, ErrorMessage = "Origem deve ter no máximo 15 letras")]
    [Required(ErrorMessage = "Origem é obrigatório")]
    public required string Origem { get; set; }

    [MinLength(3, ErrorMessage = "Destino deve ter pelo menos 3 letras")]
    [MaxLength(15, ErrorMessage = "Destino deve ter no máximo 15 letras")]
    [Required(ErrorMessage = "Destino é obrigatório")]
    public required string Destino { get; set; }

    [Range(0.01, 999.99, ErrorMessage = "Custo deve ser mínimo de 0.01 e máximo de 999.99")]
    [Required(ErrorMessage = "Custo é obrigatório")]
    public required decimal Custo { get; set; }
}

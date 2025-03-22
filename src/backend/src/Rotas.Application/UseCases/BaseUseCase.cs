using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Exceptions;

namespace Rotas.Application.UseCases;

public abstract class BaseUseCase
{
    protected private static void ValidateDto<T>(T? dto) where T : class
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "Objeto dto não pode ser nulo");

        var erros = new List<ValidationResult>();
        if (!Validator.TryValidateObject(dto, new ValidationContext(dto), erros, true))
        {
            var listaErros = erros.Select(e => e.ErrorMessage).ToList();
            var errosMsg = string.Join(", ", listaErros);

            throw new ValidacaoException(errosMsg);
        }
    }


}

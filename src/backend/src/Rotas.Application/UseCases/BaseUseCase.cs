
using System.ComponentModel.DataAnnotations;
using Rotas.Domain.Exceptions;

namespace Rotas.Application.UseCases;

public abstract class BaseUseCase<TRequest> where TRequest : class
{
    protected static void ValidateDto(TRequest? dto)
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


namespace Rotas.Domain.Exceptions;
public class ValidacaoException : Exception
{
    public ValidacaoException(string message) : base(message)
    {
    }

    public ValidacaoException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

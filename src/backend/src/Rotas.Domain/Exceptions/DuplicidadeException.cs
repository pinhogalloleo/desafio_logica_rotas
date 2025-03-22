
namespace Rotas.Domain.Exceptions;
public class DuplicidadeException : Exception
{
    public DuplicidadeException(string? message) : base(message)
    {
    }

    public DuplicidadeException(string? message, Exception? innerException) : base(message, innerException)
    { }
}

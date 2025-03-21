using System.Runtime.Serialization;

namespace Rotas.Domain.Exceptions
{
    public class NaoEncontradoException : Exception
    {
        public NaoEncontradoException(string? message) : base(message)
        {
        }

        public NaoEncontradoException(string? message, Exception? innerException) : base(message, innerException)
        { }

    }
}
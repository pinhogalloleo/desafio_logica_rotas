using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rotas.Domain.Exceptions
{
    public class DuplicidadeException:Exception
    {
        public DuplicidadeException(string? message) : base(message)
        {
        }

        public DuplicidadeException(string? message, Exception? innerException) : base(message, innerException)
        { }
    }
}
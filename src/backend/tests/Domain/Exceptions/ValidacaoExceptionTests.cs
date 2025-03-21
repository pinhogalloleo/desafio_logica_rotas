using System;
using Xunit;
using Rotas.Domain.Exceptions;

namespace Rotas.Tests.Domain.Exceptions;
public class ValidacaoExceptionTests
{
    [Fact]
    public void ValidacaoException_Should_Set_Properties_Correctly()
    {
        var exception = new ValidacaoException("Test message");
        Assert.Equal("Test message", exception.Message);
    }

    [Fact]
    public void ValidacaoException_Should_Be_Thrown()
    {
        static void action() => throw new ValidacaoException("Test message");
        var exception = Assert.Throws<ValidacaoException>(action);
        Assert.Equal("Test message", exception.Message);
    }

    [Fact]
    public void ValidacaoException_Should_Be_Thrown_With_InnerException()
    {
        var innerException = new Exception("Inner exception");
        void action() => throw new ValidacaoException("Test message", innerException);
        var exception = Assert.Throws<ValidacaoException>(action);
        Assert.Equal("Test message", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }
}
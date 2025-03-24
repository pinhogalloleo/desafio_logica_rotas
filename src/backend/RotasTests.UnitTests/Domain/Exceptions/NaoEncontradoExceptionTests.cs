
using Rotas.Domain.Exceptions;
namespace Tests.Domain.Exceptions;

public class NaoEncontradoExceptionTests
{
    [Fact]
    public void NaoEncontradoException_Should_Set_Properties_Correctly()
    {
        var exception = new NaoEncontradoException("Item not found");
        Assert.Equal("Item not found", exception.Message);
    }

    [Fact]
    public void NaoEncontradoException_Should_Be_Thrown()
    {
        static void action() => throw new NaoEncontradoException("Item not found");
        var exception = Assert.Throws<NaoEncontradoException>(action);
        Assert.Equal("Item not found", exception.Message);
    }

    [Fact]
    public void NaoEncontradoException_Should_Be_Thrown_With_InnerException()
    {
        var innerException = new Exception("Inner exception");
        void action() => throw new NaoEncontradoException("Item not found", innerException);
        var exception = Assert.Throws<NaoEncontradoException>(action);
        Assert.Equal("Item not found", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }
    
}
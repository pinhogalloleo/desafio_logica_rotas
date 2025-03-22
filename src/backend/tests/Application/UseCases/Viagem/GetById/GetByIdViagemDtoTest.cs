using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Application.UseCases.Viagens.GetById;

namespace Tests.Application.UseCases.Viagem.GetById;

public class GetByIdViagemDtoTest
{
    [Fact]
    public void TestInstantiateGetByIdViagemDto()
    {
        // Arrange
        var dto = new GetByIdViagemDto();

        // Act
        // Assert
        Assert.NotNull(dto);
    }
}

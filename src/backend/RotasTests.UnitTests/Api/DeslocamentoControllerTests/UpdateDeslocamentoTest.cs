using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rotas.Api.Controllers;
using Rotas.Application.Services;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Domain.Exceptions;

namespace Tests.Api.DeslocamentoControllerTests;

public class UpdateDeslocamentoTest
{
    [Fact]
    public async Task Test_Logical_UpdateDeslocamento_Sucess200_WhenValidDto()
    {
        // test API class DeslocamentoController, method PUT, UpdateDeslocamento
        // Arrange        
        var updateDeslocamentoDto = new UpdateDeslocamentoDto
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 10
        };
        var deslocamentoService = new Mock<IDeslocamentoService>();
        deslocamentoService.Setup(x => x.UpdateDeslocamentoAsync(updateDeslocamentoDto)).Returns(Task.CompletedTask);
        var controller = new DeslocamentoController(deslocamentoService.Object);

        // Act
        var result = await controller.UpdateDeslocamento(updateDeslocamentoDto) as OkResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        deslocamentoService.Verify(x => x.UpdateDeslocamentoAsync(updateDeslocamentoDto), Times.Once);
    }


    [Fact]
    public async Task Test_Logical_UpdateDeslocamento_BadRequest400_WhenInvalidDto()
    {
        // test API class DeslocamentoController, method PUT, UpdateDeslocamento
        // Arrange        
        var updateDeslocamentoDto = new UpdateDeslocamentoDto
        {
            Id = 1,
            Origem = "A",
            Destino = "",
            Custo = -10
        };
        var deslocamentoService = new Mock<IDeslocamentoService>();
        var expectedException = new ValidacaoException("inválido");
        deslocamentoService.Setup(x => x.UpdateDeslocamentoAsync(updateDeslocamentoDto)).ThrowsAsync(expectedException);
        var controller = new DeslocamentoController(deslocamentoService.Object);

        // Act
        var result = await controller.UpdateDeslocamento(updateDeslocamentoDto) as BadRequestObjectResult;
        var resultText = result?.Value?.ToString();        
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(expectedException.Message, resultText);
        deslocamentoService.Verify(x => x.UpdateDeslocamentoAsync(updateDeslocamentoDto), Times.Once);
    }

}

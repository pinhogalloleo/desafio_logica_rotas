using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rotas.Api.Controllers;
using Rotas.Application.Services;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;

namespace Tests.Api.DeslocamentoControllerTests;

public class InsertDeslocamentoTest
{
    [Fact]
    public async Task Test_Logical_InsertDeslocamento_Sucess200_WhenValidDto()
    {
        // Arrange
        int expectedId = 1, resultId = 0;
        var insertDeslocamentoDto = new InsertDeslocamentoDto
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 10
        };
        
        var deslocamentoService = new Mock<IDeslocamentoService>();
        deslocamentoService
            .Setup(x => x.InsertDeslocamentoAsync(insertDeslocamentoDto))
            .ReturnsAsync(expectedId);
            
        var controller = new DeslocamentoController(deslocamentoService.Object);

        // Act
        var result = await controller.InsertDeslocamento(insertDeslocamentoDto) as OkObjectResult;
        if (result != null && result.Value != null)
            resultId = (int)result.Value;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedId, resultId);
        deslocamentoService.Verify(x => x.InsertDeslocamentoAsync(insertDeslocamentoDto), Times.Once);
    }


    [Fact]
    public async Task Test_Logical_InsertDeslocamento_BadRequest400_WhenInvalidDto()
    {
        // test API class DeslocamentoController, method POST InsertDeslocamento
        // Arrange
        var deslocamentoService = new Mock<IDeslocamentoService>();
        var insertDeslocamentoDto = new InsertDeslocamentoDto
        {
            Origem = "A",
            Destino = "",
            Custo = -10
        };
        var controller = new DeslocamentoController(deslocamentoService.Object);
        controller.ModelState.AddModelError("Custo", "Custo must be greater than zero");

        // Act
        var result = await controller.InsertDeslocamento(insertDeslocamentoDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        deslocamentoService.Verify(x => x.InsertDeslocamentoAsync(insertDeslocamentoDto), Times.Never);
    }

}

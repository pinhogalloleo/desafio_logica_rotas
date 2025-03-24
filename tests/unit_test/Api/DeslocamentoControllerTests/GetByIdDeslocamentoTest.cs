
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rotas.Api.Controllers;
using Rotas.Application.Services;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;
using Rotas.Domain.Entities;
using Tests.Application.UseCases.DeslocamentoCrud.GetById;
using Xunit;

namespace Tests.Api.DeslocamentoControllerTests;

public class GetByIdDeslocamentoTest
{
    [Fact]
    public async Task GetById_ReturnsOk_WhenEntityExists()
    {
        // Arrange
        Deslocamento? resultEntity = null, expectedEntity = null;        
        expectedEntity = new Deslocamento
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 10
        };

        var mockService = new Mock<IDeslocamentoService>();
        mockService
            .Setup(service => service.GetByIdAsync(It.IsAny<GetByIdDeslocamentoDto>()))
            .ReturnsAsync(expectedEntity);

        var controller = new DeslocamentoController(mockService.Object);

        // Act
        var result = await controller.GetById(1) as OkObjectResult;
        if (result != null && result.Value != null)
            resultEntity = result?.Value as Deslocamento;

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(resultEntity);
        mockService.Verify(service => service.GetByIdAsync(It.IsAny<GetByIdDeslocamentoDto>()), Times.Once);
    }


    [Fact]
    public async Task GetById_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var mockService = new Mock<IDeslocamentoService>();
        mockService
            .Setup(service => service.GetByIdAsync(It.IsAny<GetByIdDeslocamentoDto>()))
            .ThrowsAsync(new System.Exception("Entity not found"));

        var controller = new DeslocamentoController(mockService.Object);

        // Act
        var result = await controller.GetById(1);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Entity not found", badRequestResult.Value);
    }
}
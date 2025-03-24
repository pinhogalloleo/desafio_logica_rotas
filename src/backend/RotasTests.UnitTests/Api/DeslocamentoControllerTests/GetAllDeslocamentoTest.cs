
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rotas.Api.Controllers;
using Rotas.Application.Services;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;
using Rotas.Domain.Entities;
using Tests.Application.UseCases.DeslocamentoCrud.GetById;
using Xunit;

namespace Tests.Api.DeslocamentoControllerTests;

public class GetAllDeslocamentoTest
{
    [Fact]
    public async Task GetAll_ReturnsOk_WhenExistsList()
    {
        // Arrange
        List<Deslocamento>? resultList = null, expectedList = null;
        expectedList = new List<Deslocamento>
        {
            new Deslocamento
            {
                Id = 1,
                Origem = "AAA",
                Destino = "BBB",
                Custo = 10
            },
            new Deslocamento
            {
                Id = 2,
                Origem = "CCC",
                Destino = "DDD",
                Custo = 20
            }
        };

        var mockService = new Mock<IDeslocamentoService>();
        mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedList);

        var controller = new DeslocamentoController(mockService.Object);

        // Act
        var result = await controller.GetAll() as OkObjectResult;
        if (result != null && result.Value != null)
            resultList = result?.Value as List<Deslocamento>;

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(resultList);
        Assert.Equal(expectedList.Count, resultList.Count);
        mockService.Verify(service => service.GetAllAsync(), Times.Once);
    }


    [Fact]
    public async Task GeAll_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var mockService = new Mock<IDeslocamentoService>();
        mockService
            .Setup(service => service.GetAllAsync())
            .ThrowsAsync(new System.Exception("No Entity found"));

        var controller = new DeslocamentoController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("No Entity found", badRequestResult.Value);
    }
}
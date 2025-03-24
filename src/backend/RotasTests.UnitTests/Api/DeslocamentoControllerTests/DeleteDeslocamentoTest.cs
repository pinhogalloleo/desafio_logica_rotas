using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rotas.Api.Controllers;
using Rotas.Application.Services;
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;

namespace Tests.Api.DeslocamentoControllerTests;

public class DeleteDeslocamentoTest
{
	[Fact]
	// test ok 200 when dto is valid
	public async Task Test_Logical_DeleteDeslocamento_Sucess200_WhenValidDto()
	{
        // Arrange
        var mockService = new Mock<IDeslocamentoService>();
        mockService
            .Setup(service => service.DeleteDeslocamentoAsync(It.IsAny<DeleteDeslocamentoDto>()))
            .Returns(Task.CompletedTask);

        var controller = new DeslocamentoController(mockService.Object);

        // Act
        var result = await controller.DeleteDeslocamento(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Entidade removida", okResult.Value);
	}

	    [Fact]
    public async Task DeleteDeslocamento_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var mockService = new Mock<IDeslocamentoService>();
        mockService
            .Setup(service => service.DeleteDeslocamentoAsync(It.IsAny<DeleteDeslocamentoDto>()))
            .ThrowsAsync(new System.Exception("Deletion failed"));

        var controller = new DeslocamentoController(mockService.Object);

        // Act
        var result = await controller.DeleteDeslocamento(1);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Deletion failed", badRequestResult.Value);
    }
}

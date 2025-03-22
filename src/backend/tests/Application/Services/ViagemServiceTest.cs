
using Moq;
using Rotas.Application.Services;
using Rotas.Application.UseCases.Viagens.Delete;
using Rotas.Application.UseCases.Viagens.GetAll;
using Rotas.Application.UseCases.Viagens.GetById;
using Rotas.Application.UseCases.Viagens.Insert;
using Rotas.Application.UseCases.Viagens.Update;
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using Rotas.Domain.Services;

namespace Tests.Application.Services;

public class ViagemServiceTest
{
    private static Mock<ViagemService> CreateService()
    {
        var mockRepository = new Mock<IRepositoryCrud<Viagem>>().Object;
        var mockViagemValidationService = new Mock<ViagemValidationService>(mockRepository).Object;
        var insertViagemUseCase = new Mock<InsertViagemUseCase>(mockRepository, mockViagemValidationService);
        var updateViagemUseCase = new Mock<UpdateViagemUseCase>(mockRepository, mockViagemValidationService);
        var deleteViagemUseCase = new Mock<DeleteViagemUseCase>(mockRepository);
        var getByIdViagemUseCase = new Mock<GetByIdViagemUseCase>(mockRepository);
        var getAllViagemUseCase = new Mock<GetAllViagemUseCase>(mockRepository);

        var viagemService = new Mock<ViagemService>(
            insertViagemUseCase.Object,
            updateViagemUseCase.Object,
            deleteViagemUseCase.Object,
            getByIdViagemUseCase.Object,
            getAllViagemUseCase.Object);
        return viagemService;
    }


    [Fact]
    public void InstantiateTest()
    {
        // Arrange\Act
        ViagemService viagemService = CreateService().Object;

        // Assert
        Assert.NotNull(viagemService);
    }


    [Fact]
    public async Task Test_InvokeInsert()
    {
        // Arrange
        var viagemService = CreateService();

        var dto = Mock.Of<InsertViagemDto>();
        viagemService.Setup(x => x.InsertViagemAsync(It.IsAny<InsertViagemDto>())).ReturnsAsync(1);

        // Act        
        int returnId = 0;
        Exception exception = await Record.ExceptionAsync(async () => returnId = await viagemService.Object.InsertViagemAsync(dto));

        // Assert that InsertViagemUseCase.ExecuteAsync() was called Once within InsertViagemAsync
        viagemService.Verify(x => x.InsertViagemAsync(It.IsAny<InsertViagemDto>()), Times.Once);

        // Assert
        Assert.Null(exception);
        Assert.Equal(1, returnId);



        viagemService.Verify(x => x.InsertViagemAsync(It.IsAny<InsertViagemDto>()), Times.Once);
    }


    [Fact]
    public async Task Test_InvokeUpdate()
    {
        // Arrange
        var viagemService = CreateService();
        var updateViagemUseCase = new Mock<UpdateViagemUseCase>();
        var dto = Mock.Of<UpdateViagemDto>();
        updateViagemUseCase.Setup(x => x.ExecuteAsync(dto)).Returns(Task.CompletedTask);
        viagemService.Setup(x => x.UpdateViagemAsync(It.IsAny<UpdateViagemDto>())).Returns(Task.CompletedTask);

        // Act
        Exception exception = await Record.ExceptionAsync(async () => await viagemService.Object.UpdateViagemAsync(dto));

        // Assert
        Assert.Null(exception);
        updateViagemUseCase.Verify(x => x.ExecuteAsync(It.IsAny<UpdateViagemDto>()), Times.Once);
    }


    [Fact]
    public async Task Test_InvokeDelete()
    {
        // Arrange
        var viagemService = CreateService();
        var deleteViagemUseCase = new Mock<DeleteViagemUseCase>();
        var dto = Mock.Of<DeleteViagemDto>();
        deleteViagemUseCase.Setup(x => x.ExecuteAsync(It.IsAny<DeleteViagemDto>())).Returns(Task.CompletedTask);

        // Act
        Exception exception = await Record.ExceptionAsync(async () => await viagemService.Object.DeleteViagemAsync(dto));

        // Assert
        Assert.Null(exception);
        deleteViagemUseCase.Verify(x => x.ExecuteAsync(It.IsAny<DeleteViagemDto>()), Times.Once);
    }


    [Fact]
    public async Task Test_InvokeGetById()
    {
        // Arrange
        var viagemService = CreateService();
        var getByIdViagemUseCase = new Mock<GetByIdViagemUseCase>();
        var dto = Mock.Of<GetByIdViagemDto>();
        getByIdViagemUseCase.Setup(x => x.ExecuteAsync(It.IsAny<GetByIdViagemDto>())).ReturnsAsync(Mock.Of<Viagem>());

        // Act
        Exception exception = await Record.ExceptionAsync(async () => await viagemService.Object.GetByIdAsync(dto));

        // Assert
        Assert.Null(exception);
        getByIdViagemUseCase.Verify(x => x.ExecuteAsync(It.IsAny<GetByIdViagemDto>()), Times.Once);
    }


    [Fact]
    public async Task Test_InvokeGetAll()
    {
        // Arrange
        var viagemService = CreateService();
        var getAllViagemUseCase = new Mock<GetAllViagemUseCase>();
        getAllViagemUseCase.Setup(x => x.ExecuteAsync()).ReturnsAsync(new List<Viagem>());

        // Act
        Exception exception = await Record.ExceptionAsync(async () => await viagemService.Object.GetAllAsync());

        // Assert
        Assert.Null(exception);
        getAllViagemUseCase.Verify(x => x.ExecuteAsync(), Times.Once);
    }

}

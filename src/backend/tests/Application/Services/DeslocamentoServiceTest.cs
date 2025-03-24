
using Moq;
using Rotas.Application.Services;
using Rotas.Application.UseCases;
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;
using Rotas.Application.UseCases.DeslocamentoCrud.GetAll;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Domain.Entities;

namespace Tests.Application.Services;

public class DeslocamentoServiceTest
{
    private static DeslocamentoService CreateService(
        IUseCase<InsertDeslocamentoDto, Task<int>>? mockInsert = null,
        IUseCase<UpdateDeslocamentoDto, Task>? mockUpdate = null,
        IUseCase<DeleteDeslocamentoDto, Task>? mockDelete = null,
        IUseCase<GetByIdDeslocamentoDto, Task<Deslocamento>>? mockGetById = null,
        IUseCase<Task<List<Deslocamento>>>? mockGetAll = null)
    {

        IUseCase<InsertDeslocamentoDto, Task<int>> mockInsertUseCase;
        IUseCase<UpdateDeslocamentoDto, Task> mockUpdateUseCase;
        IUseCase<DeleteDeslocamentoDto, Task> mockDeleteUseCase;
        IUseCase<GetByIdDeslocamentoDto, Task<Deslocamento>> mockGetByIdDeslocamentoUseCase;
        IUseCase<Task<List<Deslocamento>>> mockGetAllDeslocamentoUseCase;


        if (mockInsert == null)
            mockInsertUseCase = new Mock<IUseCase<InsertDeslocamentoDto, Task<int>>>().Object;
        else
            mockInsertUseCase = mockInsert;

        if (mockUpdate == null)
            mockUpdateUseCase = new Mock<IUseCase<UpdateDeslocamentoDto, Task>>().Object;
        else
            mockUpdateUseCase = mockUpdate;

        if (mockDelete == null)
            mockDeleteUseCase = new Mock<IUseCase<DeleteDeslocamentoDto, Task>>().Object;
        else
            mockDeleteUseCase = mockDelete;

        if (mockGetById == null)
            mockGetByIdDeslocamentoUseCase = new Mock<IUseCase<GetByIdDeslocamentoDto, Task<Deslocamento>>>().Object;
        else
            mockGetByIdDeslocamentoUseCase = mockGetById;

        if (mockGetAll == null)
            mockGetAllDeslocamentoUseCase = new Mock<IUseCase<Task<List<Deslocamento>>>>().Object;
        else
            mockGetAllDeslocamentoUseCase = mockGetAll;


        // temos que testar a classe concreta, validar o algoritmo que ela executa
        // para isso, injetamos fakes nela de todas as interfaces externas que ela usa
        // e configuramos o comportamento dessas interfaces, como "caixa preta" simulada
        var deslocamentoService = new DeslocamentoService(
            mockInsertUseCase,
            mockUpdateUseCase,
            mockDeleteUseCase,
            mockGetByIdDeslocamentoUseCase,
            mockGetAllDeslocamentoUseCase
        );

        return deslocamentoService;
    }


    [Fact]
    public void InstantiateTest()
    {
        // Arrange\Act
        DeslocamentoService deslocamentoService = CreateService();

        // Assert
        Assert.NotNull(deslocamentoService);
    }


    [Fact]
    public async Task Test_InvokeInsert()
    {
        // Arrange
        var mockInsertDeslocamentoUseCase = new Mock<IUseCase<InsertDeslocamentoDto, Task<int>>>();
        var insertDeslocamentoUseCase = mockInsertDeslocamentoUseCase.Object;
        mockInsertDeslocamentoUseCase.Setup(x => x.ExecuteAsync(It.IsAny<InsertDeslocamentoDto>())).ReturnsAsync(1);

        var dto = new InsertDeslocamentoDto()
        {
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };

        var deslocamentoService = CreateService(mockInsert: insertDeslocamentoUseCase);

        // Act        
        int returnId = 0;
        Exception exception = await Record.ExceptionAsync(async () => returnId = await deslocamentoService.InsertDeslocamentoAsync(dto));

        // Assert
        Assert.Null(exception);
        Assert.Equal(1, returnId);

        // assert that internal interface mock was invoked once
        mockInsertDeslocamentoUseCase.Verify(x => x.ExecuteAsync(It.IsAny<InsertDeslocamentoDto>()), Times.Once);
    }


    [Fact]
    public async Task Test_InvokeUpdate()
    {
        // Arrange
        var dto = new UpdateDeslocamentoDto()
        {
            Id = 1,
            Origem = "AAA",
            Destino = "BBB",
            Custo = 150
        };

        var mockUpdateDeslocamentoUseCase = new Mock<IUseCase<UpdateDeslocamentoDto, Task>>();
        var updateDeslocamentoUseCase = mockUpdateDeslocamentoUseCase.Object;
        mockUpdateDeslocamentoUseCase.Setup(x => x.ExecuteAsync(dto)).Returns(Task.CompletedTask);

        var deslocamentoService = CreateService(mockUpdate: updateDeslocamentoUseCase);


        // Act
        Exception exception = await Record.ExceptionAsync(async () => await deslocamentoService.UpdateDeslocamentoAsync(dto));

        // Assert
        Assert.Null(exception);
        mockUpdateDeslocamentoUseCase.Verify(x => x.ExecuteAsync(dto), Times.Once);
    }


    [Fact]
    public async Task Test_InvokeDelete()
    {
        // Arrange
        var dto = new DeleteDeslocamentoDto()
        {
            Id = 1
        };
        var mockDeleteDeslocamentoUseCase = new Mock<IUseCase<DeleteDeslocamentoDto, Task>>();
        var deleteDeslocamentoUseCase = mockDeleteDeslocamentoUseCase.Object;
        mockDeleteDeslocamentoUseCase.Setup(x => x.ExecuteAsync(dto)).Returns(Task.CompletedTask);

        var deslocamentoService = CreateService(mockDelete: deleteDeslocamentoUseCase);

        // Act
        Exception exception = await Record.ExceptionAsync(async () => await deslocamentoService.DeleteDeslocamentoAsync(dto));

        // Assert
        Assert.Null(exception);
        mockDeleteDeslocamentoUseCase.Verify(x => x.ExecuteAsync(dto), Times.Once);
    }


    [Fact]
    public async Task Test_InvokeGetById()
    {
        // Arrange
        var dtoRequest = new GetByIdDeslocamentoDto() { Id = 1 };
        var deslocamento = new Deslocamento() { Id = 1, Origem = "AAA", Destino = "BBB", Custo = 150 };
        var mockGetByIdDeslocamentoUseCase = new Mock<IUseCase<GetByIdDeslocamentoDto, Task<Deslocamento>>>();
        var getByIdDeslocamentoUseCase = mockGetByIdDeslocamentoUseCase.Object;

        mockGetByIdDeslocamentoUseCase.Setup(x => x.ExecuteAsync(dtoRequest)).ReturnsAsync(deslocamento);

        var deslocamentoService = CreateService(mockGetById: getByIdDeslocamentoUseCase);

        // Act
        Deslocamento? retorno = null;
        Exception exception = await Record.ExceptionAsync(async () => retorno = await deslocamentoService.GetByIdAsync(dtoRequest));

        // Assert
        Assert.NotNull(retorno);
        Assert.Equal(1, retorno.Id);
        Assert.Null(exception);
        mockGetByIdDeslocamentoUseCase.Verify(x => x.ExecuteAsync(dtoRequest), Times.Once);
    }


    [Fact]
    public async Task Test_InvokeGetAll()
    {
        // Arrange
        var listaDeslocamentoEsperada = new List<Deslocamento>
        {
            new Deslocamento() { Id = 1, Origem = "AAA", Destino = "BBB", Custo = 150 },
            new Deslocamento() { Id = 2, Origem = "CCC", Destino = "DDD", Custo = 200 }
        };

        var mockGetAllDeslocamentoUseCase = new Mock<IUseCase<Task<List<Deslocamento>>>>();
        var getAllDeslocamentoUseCase = mockGetAllDeslocamentoUseCase.Object;
        mockGetAllDeslocamentoUseCase.Setup(x => x.ExecuteAsync()).ReturnsAsync(listaDeslocamentoEsperada);

        var deslocamentoService = CreateService(mockGetAll: getAllDeslocamentoUseCase);

        // Act
        List<Deslocamento>? listaRetorno = null;
        Exception exception = await Record.ExceptionAsync(async () => listaRetorno = await deslocamentoService.GetAllAsync());

        // Assert
        Assert.Null(exception);
        Assert.NotNull(listaRetorno);
        Assert.Equal(2, listaRetorno.Count);
        Assert.Equal(1, listaRetorno[0].Id);
        Assert.Equal(2, listaRetorno[1].Id);
        mockGetAllDeslocamentoUseCase.Verify(x => x.ExecuteAsync(), Times.Once);
    }

}


using Moq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Rotas.Domain.Interfaces;
using Rotas.DataAccess.FileDataAccess.DependencyInjection;
using Rotas.Domain.Entities;

namespace Tests.Infra.DataAccess.FileDataAccess;

public class DependencyInjectionTest
{
    // test setup of Rotas.DataAccess.FileDataAccess.RepositoryCrudViagem
    [Fact]
    public void TestSetupFileDataAccess_Success()
    {
        // Arrange
        var services = new ServiceCollection();
        // var configuration = new ConfigurationBuilder().Build()
        var configuration = new Mock<IConfiguration>();
        configuration.Setup(x => x["PersistenceFileName"]).Returns("test.json");

        // Act
        services.SetupInfraFileDataAccess(configuration.Object);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var repository = serviceProvider.GetService<IRepositoryCrud<Deslocamento>>();
        Assert.NotNull(repository);
    }

    // test same setup, but with IConfiguration null
    [Fact]
    public void TestSetupFileDataAccess_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        IConfiguration? config = GetConfiguration();
        Action act = () => services.SetupInfraFileDataAccess(config);

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(act);
        Assert.Equal("configuration", ex.ParamName);
    }

    private static IConfiguration? GetConfiguration() => null;

    // test same stup, but now returning an empty string for the parameter PersistenceFileName
    [Fact]
    public void TestSetupFileDataAccess_ThrowsArgumentNullExceptionForPersistenceFileName()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new Mock<IConfiguration>();
        configuration.Setup(x => x["PersistenceFileName"]).Returns("");

        // Act
        Action act = () => services.SetupInfraFileDataAccess(configuration.Object);

        // Assert
        var ex = Assert.Throws<ArgumentNullException>(act);
        Assert.Equal("configuration", ex.ParamName);
    }

}

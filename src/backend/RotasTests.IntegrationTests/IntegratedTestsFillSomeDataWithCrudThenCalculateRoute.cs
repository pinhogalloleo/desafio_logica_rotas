
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Rotas.Api;
using Rotas.Domain.Entities;
using Rotas.Domain.Entities.Grafo;
using RotasTests.IntegrationTests;

// we DONT want parallel testing, because of logical sequence of data filling\binding
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace RotasTests.IntegrationTests;


[Collection("IntegrationTests")]
public class IntegratedTestsFillSomeDataWithCrudThenCalculateRoute(TestDatabaseFixture fixture) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly TestDatabaseFixture _fixture = fixture;
    private readonly string endpointCrud = "/api/deslocamento";
    private readonly string endpointCalculoRota = "/api/melhorRota?origem={Origem}&destino={Destino}";

    [Fact]
    public async Task Orchestrate()
    {
        await TestCrudActions();
        await TestCalculoRota();
    }

    private async Task TestCalculoRota()
    {
        // Erase all test data
        await Erase();

        // Step 3: Insert the first entity
        await InsertOne("AAA", "CCC", 50);
        await InsertOne("AAA", "BBB", 10);
        await InsertOne("BBB", "CCC", 10);

        // test routes AAA to BBB (only 1 trip, cost 10)
        Rota? rotaAAABBB = await CalculoRota("AAA", "BBB");
        Assert.NotNull(rotaAAABBB);
        Assert.True(rotaAAABBB.CustoTotal == 10);
        Assert.Contains("AAA", rotaAAABBB.Caminho);
        Assert.Contains("BBB", rotaAAABBB.Caminho);

        // test AAA to CCC (two displacements, cost 20)
        Rota? rotaAAACCC = await CalculoRota("AAA", "CCC");
        Assert.NotNull(rotaAAACCC);
        Assert.True(rotaAAACCC.CustoTotal == 20);
        Assert.Contains("AAA", rotaAAACCC.Caminho);
        Assert.Contains("BBB", rotaAAACCC.Caminho);
        Assert.Contains("CCC", rotaAAACCC.Caminho);

        // Erase all test data
        await Erase();
    }



    private async Task TestCrudActions()
    {
        // Erase all test data
        await Erase();

        // Step 2: Test GetAll when the database is empty
        await TestGetAll_EmptyDb();

        // Step 3: Insert the first entity
        var id1 = await InsertOne("AAA", "BBB", 10);

        // Step 4: Get the first entity by ID
        await GetById(id1);

        // Step 5: Test GetAll with one entity
        await TestGetAll_OneEntity(expectedCount: 1);

        // Step 6: Insert a second entity
        var id2 = await InsertOne("CCC", "DDD", 20);

        // Step 7: Test GetAll with two entities
        await TestGetAll_OneEntity(expectedCount: 2);

        // Step 8: Update the second entity
        var entity2 = await GetById(id2);
        entity2.Origem = "upd2";
        await Update(entity2);

        // Step 9: Delete the first entity
        await Delete(id1);

        // Step 10: Test GetAll with one remaining entity
        await TestGetAll_OneEntity(expectedCount: 1);

        // Erase all test data
        await Erase();
    }


    private async Task<Rota?> CalculoRota(string origem, string destino)
    {
        var url = endpointCalculoRota.Replace("{Origem}", origem).Replace("{Destino}", destino);
        var (rota, statusCode, errorMessage) = await _fixture.ApiHelper.GetAsync<Rota>(url);
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.NotNull(rota);
        Assert.Null(errorMessage);
        return rota;
    }


    private async Task Erase()
    {
        await _fixture.EraseAllDataAsync();
    }

    private async Task TestGetAll_EmptyDb()
    {
        var (deslocamentos, statusCode, errorMessage) = await _fixture.ApiHelper.GetAsync<List<Deslocamento>>(endpointCrud);
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.NotNull(deslocamentos);
        Assert.Empty(deslocamentos);
        Assert.Null(errorMessage);
    }

    private async Task<int> InsertOne(string origem, string destino, decimal custo)
    {
        var dto = new { Origem = origem, Destino = destino, Custo = custo };
        var (id, statusCode, errorMessage) = await _fixture.ApiHelper.PostAsync<object, int>(endpointCrud, dto);
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.NotEqual(0, id);
        Assert.Null(errorMessage);
        return id;
    }

    private async Task<Deslocamento> GetById(int id)
    {
        var (entity, statusCode, errorMessage) = await _fixture.ApiHelper.GetAsync<Deslocamento>($"{endpointCrud}/{id}");

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.NotNull(entity);
        Assert.Null(errorMessage);
        return entity!;
    }

    private async Task TestGetAll_OneEntity(int expectedCount)
    {
        var (deslocamentos, statusCode, errorMessage) = await _fixture.ApiHelper.GetAsync<List<Deslocamento>>(endpointCrud);
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.NotNull(deslocamentos);
        Assert.Equal(expectedCount, deslocamentos.Count);
        Assert.Null(errorMessage);
    }

    private async Task Update(Deslocamento entity)
    {
        var (statusCode, errorMessage) = await _fixture.ApiHelper.PutAsync(endpointCrud, entity);
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Null(errorMessage);
    }

    private async Task Delete(int id)
    {
        var (statusCode, errorMessage) = await _fixture.ApiHelper.DeleteAsync($"{endpointCrud}/{id}");
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Null(errorMessage);
    }


}

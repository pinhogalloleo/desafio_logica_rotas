
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Rotas.Api;
using Rotas.Domain.Entities;

namespace RotasTests.IntegrationTests;

public class TestDatabaseFixture
{
    public HttpClient Client { get; set; }
    public ApiHelper ApiHelper { get; set; }

    public TestDatabaseFixture()
    {
        var factory = new WebApplicationFactory<Program>();
        this.Client = factory.CreateClient();
        this.ApiHelper = new ApiHelper(this.Client);
    }

    internal async Task EraseAllDataAsync()
    {
        var (deslocamentos, statusCode, errorMessage) = await ApiHelper.GetAsync<List<Deslocamento>>("/api/deslocamento");
        if (statusCode != HttpStatusCode.OK)
            throw new Exception($"Failed to retrieve records for cleanup: {errorMessage}");

        if (deslocamentos == null)
            return;

        foreach (var item in deslocamentos)
        {
            var deleteUrl = $"/api/deslocamento/{item.Id}";
            var (deleteStatusCode, deleteErrorMessage) = await ApiHelper.DeleteAsync(deleteUrl);
            if (deleteStatusCode != HttpStatusCode.OK)
                throw new Exception($"Failed to delete record {item.Id}: {deleteErrorMessage}");
        }
    }


}


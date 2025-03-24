using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RotasTests.IntegrationTests;

public class ApiHelper
{
    private readonly HttpClient _client;

    public ApiHelper(HttpClient client)
    {
        _client = client;
    }

    public async Task<(TResponse? Data, HttpStatusCode StatusCode, string? ErrorMessage)> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<TResponse>();
                return (responseData, response.StatusCode, null);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (default, response.StatusCode, errorMessage);
            }
        }
        catch (Exception ex)
        {
            return (default, HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<(TResponse? Data, HttpStatusCode StatusCode, string? ErrorMessage)> GetAsync<TResponse>(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<TResponse>();
                return (responseData, response.StatusCode, null);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (default, response.StatusCode, errorMessage);
            }
        }
        catch (Exception ex)
        {
            return (default, HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<(HttpStatusCode StatusCode, string? ErrorMessage)> PutAsync<TRequest>(string url, TRequest data)
    {
        try
        {
            var response = await _client.PutAsJsonAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                return (response.StatusCode, null);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (response.StatusCode, errorMessage);
            }
        }
        catch (Exception ex)
        {
            return (HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<(HttpStatusCode StatusCode, string? ErrorMessage)> DeleteAsync(string url)
    {
        try
        {
            var response = await _client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return (response.StatusCode, null);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (response.StatusCode, errorMessage);
            }
        }
        catch (Exception ex)
        {
            return (HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}



using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace Rotas.DataAccess.FileDataAccess;

public class RepositoryCrudViagem : IRepositoryCrud<Viagem>, IDisposable
{
    private readonly string filePath;
    private bool disposed = false;
    private List<Viagem> viagens = new List<Viagem>();


    public RepositoryCrudViagem(string path)
    {
        this.filePath = path;
        LoadData();
    }


    private void LoadData()
    {
        viagens.Clear();

        if (!File.Exists(filePath))
        {
            System.IO.File.Create(filePath).Dispose();
            return;
        }

        var json = File.ReadAllText(filePath);

        if (!string.IsNullOrEmpty(json))
            viagens = JsonSerializer.Deserialize<List<Viagem>>(json) ?? new List<Viagem>();
    }


    private void FlushData()
    {
        var json = JsonSerializer.Serialize(viagens) ?? string.Empty;
        File.WriteAllText(filePath, json);
    }


    // implement IDisposable methods and call "FlushData" method to force data saving
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                FlushData();
            }
            disposed = true;
        }
    }


    public async Task<List<Viagem>> GetAllAsync()
    {
        return await Task.FromResult(viagens);
    }


    public async Task<Viagem> GetByIdAsync(int id)
    {
        var result = viagens.FirstOrDefault(x => x.Id == id);
        if (result == null)
            throw new NaoEncontradoException($"Viagem não encontrada pelo id [{id}]");

        return await Task.FromResult(result);
    }


    public async Task<int> InsertAsync(Viagem? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Objeto viagem não pode ser nulo");

        entity.Id = viagens.Count + 1;
        viagens.Add(entity);
        return await Task.FromResult(entity.Id);
    }


    public async Task<Viagem> UpdateAsync(Viagem? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Objeto viagem não pode ser nulo");

        var index = viagens.FindIndex(x => x.Id == entity.Id);

        if (index < 0)
            throw new NaoEncontradoException($"Viagem não encontrada para atualização pelo id [{entity.Id}]");

        viagens[index] = entity;
        return await Task.FromResult(entity);
    }


    public async Task DeleteAsync(int id)
    {
        var index = viagens.FindIndex(x => x.Id == id);
        if (index < 0)
            throw new NaoEncontradoException($"Viagem não encontrada para exclusão pelo id [{id}]");

        viagens.RemoveAt(index);
        await Task.CompletedTask;
    }


    public async Task<List<Viagem>> SearchByExpressionAsync(Expression<Func<Viagem, bool>> filter)
    {
        return await Task.FromResult(viagens.Where(filter.Compile()).ToList());
    }


}//..class

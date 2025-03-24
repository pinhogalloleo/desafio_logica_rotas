
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rotas.DataAccess.FileDataAccess;

public class RepositoryCrudDeslocamento : IRepositoryCrud<Deslocamento>, IDisposable
{
    private readonly string filePath;
    private bool disposed = false;
    private List<Deslocamento> deslocamentos = new List<Deslocamento>();
    


    public RepositoryCrudDeslocamento(string path)
    {
        this.filePath = path;
        LoadData();
    }


    private void LoadData()
    {
        deslocamentos.Clear();

        if (!File.Exists(filePath))
        {
            System.IO.File.Create(filePath).Dispose();
            return;
        }

        var json = File.ReadAllText(filePath);

        if (!string.IsNullOrEmpty(json))
            deslocamentos = JsonSerializer.Deserialize<List<Deslocamento>>(json) ?? new List<Deslocamento>();
    }


    private void FlushData()
    {
        var json = JsonSerializer.Serialize(deslocamentos) ?? string.Empty;
        File.WriteAllText(filePath, json);
    }



    public async Task<List<Deslocamento>> GetAllAsync()
    {
        return await Task.FromResult(deslocamentos);
    }


    public async Task<Deslocamento?> GetByIdAsync(int id)
    {
        var result = deslocamentos.FirstOrDefault(x => x.Id == id);

        return await Task.FromResult(result);
    }


    public async Task<int> InsertAsync(Deslocamento? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Objeto deslocamento não pode ser nulo");

        if (deslocamentos.Count > 0)
            entity.Id = deslocamentos.Max(x => x.Id) + 1;
        else
            entity.Id = 1;

        deslocamentos.Add(entity);
        return await Task.FromResult(entity.Id);
    }


    public async Task UpdateAsync(Deslocamento? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Objeto deslocamento não pode ser nulo");

        await Task.Run(() =>
        {
            var index = deslocamentos.FindIndex(x => x.Id == entity.Id);

            if (index < 0)
                throw new NaoEncontradoException($"Deslocamento não encontrada para atualização pelo id [{entity.Id}]");

            deslocamentos[index] = entity;
        });
    }


    public async Task DeleteAsync(int id)
    {
        var index = deslocamentos.FindIndex(x => x.Id == id);
        if (index < 0)
            throw new NaoEncontradoException($"Deslocamento não encontrada para exclusão pelo id [{id}]");

        await Task.Run(() =>
        {
            deslocamentos.RemoveAt(index);
        });
    }


    public async Task<List<Deslocamento>> SearchByExpressionAsync(Expression<Func<Deslocamento, bool>> filter)
    {
        return await Task.FromResult(deslocamentos.Where(filter.Compile()).ToList());
    }



    // Correct implementation of IDisposable
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                FlushData();
            }

            // No unmanaged resources to clean up in this case

            disposed = true;
        }
    }

}//..class

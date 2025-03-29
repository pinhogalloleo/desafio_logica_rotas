using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Rotas.DataAccess.EF.Data;

public class RepositoryCrud : IRepositoryCrud<Deslocamento>
{
    private readonly RotasDbContext _context;

    public RepositoryCrud(RotasDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Deslocamento>> GetAllAsync()
    {
        return await _context.Set<Deslocamento>().ToListAsync();
    }

    public async Task<Deslocamento?> GetByIdAsync(int id)
    {
        return await _context.Set<Deslocamento>().FindAsync(id);
    }

    public async Task<int> InsertAsync(Deslocamento entity)
    {
        await _context.Set<Deslocamento>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Deslocamento entity)
    {
        _context.Set<Deslocamento>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            throw new Exception("Entity not found");

        _context.Set<Deslocamento>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Deslocamento>> SearchByExpressionAsync(Expression<Func<Deslocamento, bool>> filter)
    {
        return await _context.Set<Deslocamento>().Where(filter).ToListAsync();
    }
}

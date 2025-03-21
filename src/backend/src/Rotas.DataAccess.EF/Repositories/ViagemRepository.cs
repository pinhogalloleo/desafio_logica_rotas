using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Rotas.Data.EF.Repositories
{
    public class ViagemRepository : IRepositoryCrud<Viagem>
    {
        private readonly AppViagensDbContext _context;

        public ViagemRepository(AppViagensDbContext context)
        {
            _context = context;
        }

        public async Task<List<Viagem>> GetAllAsync()
        {
            return await _context.DbSetViagens.ToListAsync();
        }

        public async Task<List<Viagem>> SearchFilteredAsync(Expression<Func<Viagem, bool>> filter)
        {
            return await _context.DbSetViagens.Where(filter).ToListAsync();
        }

        public async Task<Viagem> GetByIdAsync(int id)
        {
            var entity = await _context.DbSetViagens.FindAsync(id);
            return entity ?? throw new KeyNotFoundException($"Viagem with ID {id} was not found.");
        }

        public async Task<int> CreateAsync(Viagem entity)
        {
            _context.DbSetViagens.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Viagem> UpdateAsync(Viagem entity)
        {
            _context.DbSetViagens.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.DbSetViagens.Remove(entity);
            await _context.SaveChangesAsync();
        }


    }//...class
}//...namespace
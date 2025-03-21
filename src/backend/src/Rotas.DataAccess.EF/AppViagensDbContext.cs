using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rotas.Domain.Entities;

namespace Rotas.Data.EF
{
    public class AppViagensDbContext : DbContext
    {
        public AppViagensDbContext(DbContextOptions<AppViagensDbContext> options) : base(options)
        {
        }

        public DbSet<Viagem> DbSetViagens { get; set; }
    }

}
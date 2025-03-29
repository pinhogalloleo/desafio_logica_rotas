using Microsoft.EntityFrameworkCore;
using Rotas.Domain.Entities;

namespace Rotas.DataAccess.EF.Data;

public class RotasDbContext : DbContext
{
    public DbSet<Deslocamento> Deslocamentos { get; set; }

    public RotasDbContext(DbContextOptions<RotasDbContext> options) : base(options)
    {
    }
}

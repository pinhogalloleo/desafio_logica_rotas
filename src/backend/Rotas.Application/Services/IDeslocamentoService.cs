
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Domain.Entities;

namespace Rotas.Application.Services
{
    public interface IDeslocamentoService
    {
        Task<int> InsertDeslocamentoAsync(InsertDeslocamentoDto dto);
        Task UpdateDeslocamentoAsync(UpdateDeslocamentoDto dto);
        Task DeleteDeslocamentoAsync(DeleteDeslocamentoDto dto);
        Task<Deslocamento> GetByIdAsync(GetByIdDeslocamentoDto dto);
        Task<IEnumerable<Deslocamento>> GetAllAsync();

    }
}

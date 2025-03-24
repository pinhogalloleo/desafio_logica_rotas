
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;


namespace Rotas.Application.UseCases.DeslocamentoCrud.Delete;

public class DeleteDeslocamentoUseCase : BaseUseCase<DeleteDeslocamentoDto>, IUseCase<DeleteDeslocamentoDto, Task>
{
    private readonly IRepositoryCrud<Deslocamento> _repository;
    public DeleteDeslocamentoUseCase(IRepositoryCrud<Deslocamento> repository)
    {
        this._repository = repository;
    }


    public async Task ExecuteAsync(DeleteDeslocamentoDto? dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "Objeto dto não pode ser nulo");

        ValidateDto(dto);

        var deslocamentoExistente = await _repository.GetByIdAsync(dto.Id);
        if (deslocamentoExistente == null)
            throw new NaoEncontradoException($"Deslocamento não encontrada pelo Id [{dto.Id}]");

        await _repository.DeleteAsync(dto.Id);
    }

}


namespace Rotas.Application.UseCases;
public interface IUseCase<in TRequest, out TResponse>
{
    TResponse ExecuteAsync(TRequest request);
}

public interface IUseCase<out TResponse>
{
    TResponse ExecuteAsync();
}
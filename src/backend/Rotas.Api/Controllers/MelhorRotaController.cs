
using Microsoft.AspNetCore.Mvc;
using Rotas.Application.Services;
using Rotas.Application.UseCases.CalculoRota;

namespace Rotas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MelhorRotaController(ICalculoMelhorRotaService calculoMelhorRotaService) : ControllerBase
{
    private readonly ICalculoMelhorRotaService _calculoMelhorRotaService = calculoMelhorRotaService;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CalculoRotaDto dto)
    {
        try
        {
            var rota = await _calculoMelhorRotaService.CalcularMelhorRotaAsync(dto);
            if (rota == null)
                return NotFound();

            return Ok(rota);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

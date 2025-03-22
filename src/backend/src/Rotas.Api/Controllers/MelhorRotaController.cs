using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rotas.Application.UseCases.CalculoRota;
using Rotas.Application.UseCases.CalculoRota.Dto;

namespace Rotas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MelhorRotaController(CalculoRotaUseCase calculoRotaUseCase) : ControllerBase
{
    private readonly CalculoRotaUseCase _calculoRotaUseCase = calculoRotaUseCase;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CalculoRotaDto dto)
    {
        try
        {
            var rota = await _calculoRotaUseCase.ExecuteAsync(dto);
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

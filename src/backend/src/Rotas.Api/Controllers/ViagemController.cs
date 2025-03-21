using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rotas.Application.Dtos;
using Rotas.Application.UseCases.Viagens;

namespace Rotas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViagemController(AddViagemUseCase addViagemUseCase) : ControllerBase
    {
        private readonly AddViagemUseCase _addViagemUseCase = addViagemUseCase;

        [HttpPost]
        public async Task<IActionResult> AddViagem([FromBody] ViagemDto viagemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _addViagemUseCase.ExecuteAsync(viagemDto);
                return Ok(new { Id = id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }//..AddViagem [HttpPost]



    } //..class
} //..namespace
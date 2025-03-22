
using Microsoft.AspNetCore.Mvc;
using Rotas.Application.Services;
using Rotas.Application.UseCases.Viagens.Insert;
using Rotas.Application.UseCases.Viagens.Update;
using Rotas.Application.UseCases.Viagens.Delete;
using Rotas.Application.UseCases.Viagens.GetById;


namespace Rotas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ViagemController(ViagemService viagemService) : ControllerBase
{
    private readonly ViagemService _viagemService = viagemService;
    

    [HttpPost]
    public async Task<IActionResult> InsertViagem([FromBody] InsertViagemDto insertDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _viagemService.InsertViagemAsync(insertDto);
            return Ok(id);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..AddViagem [HttpPost]


    [HttpPut]
    public async Task<IActionResult> UpdateViagem([FromBody] UpdateViagemDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _viagemService.UpdateViagemAsync(updateDto);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..UpdateViagem [HttpPut]


    [HttpDelete("id")]
    public async Task<IActionResult> DeleteViagem(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var deleteDto = new DeleteViagemDto(){ Id = id };
            await _viagemService.DeleteViagemAsync(deleteDto);
            return Ok("Entidade removida");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..DeleteViagem [HttpDelete]


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var dto = new GetByIdViagemDto(){ Id = id };
            var entity = await _viagemService.GetByIdAsync(dto);
            return Ok(entity);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..GetById [HttpGet]


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await _viagemService.GetAllAsync();
            return Ok(list);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..GetAll [HttpGet]



} //..class
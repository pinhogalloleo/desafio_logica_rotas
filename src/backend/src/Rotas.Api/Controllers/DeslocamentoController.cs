
using Microsoft.AspNetCore.Mvc;
using Rotas.Application.Services;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;


namespace Rotas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeslocamentoController(IDeslocamentoService deslocamentoService) : ControllerBase
{
    private readonly IDeslocamentoService _deslocamentoService = deslocamentoService;
    

    [HttpPost]
    public async Task<IActionResult> InsertDeslocamento([FromBody] InsertDeslocamentoDto insertDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _deslocamentoService.InsertDeslocamentoAsync(insertDto);
            return Ok(id);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..AddDeslocamento [HttpPost]


    [HttpPut]
    public async Task<IActionResult> UpdateDeslocamento([FromBody] UpdateDeslocamentoDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _deslocamentoService.UpdateDeslocamentoAsync(updateDto);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..UpdateDeslocamento [HttpPut]


    [HttpDelete("id")]
    public async Task<IActionResult> DeleteDeslocamento(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var deleteDto = new DeleteDeslocamentoDto(){ Id = id };
            await _deslocamentoService.DeleteDeslocamentoAsync(deleteDto);
            return Ok("Entidade removida");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..DeleteDeslocamento [HttpDelete]


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var dto = new GetByIdDeslocamentoDto(){ Id = id };
            var entity = await _deslocamentoService.GetByIdAsync(dto);
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
            var list = await _deslocamentoService.GetAllAsync();
            return Ok(list);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..GetAll [HttpGet]



} //..class
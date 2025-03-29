
using Microsoft.AspNetCore.Mvc;
using Rotas.Application.Services;
using Rotas.Application.UseCases.DeslocamentoCrud.Insert;
using Rotas.Application.UseCases.DeslocamentoCrud.Update;
using Rotas.Application.UseCases.DeslocamentoCrud.Delete;
using Rotas.Application.UseCases.DeslocamentoCrud.GetById;

namespace Rotas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeslocamentoController(IDeslocamentoService deslocamentoService, Serilog.ILogger logger) : ControllerBase
{
    private readonly IDeslocamentoService _deslocamentoService = deslocamentoService;
    private readonly Serilog.ILogger _logger = logger;

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> InsertDeslocamento([FromBody] InsertDeslocamentoDto insertDto)
    {
        _logger.Debug("Trying action {action} with dto: {dto}", "insert", insertDto);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            int id = await _deslocamentoService.InsertDeslocamentoAsync(insertDto);
            _logger.Information("action {action} succeeded with dto: {dto} - generated id {id}", "insert", insertDto, id);
            return Ok(id);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..AddDeslocamento [HttpPost]


    [HttpPut]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateDeslocamento([FromBody] UpdateDeslocamentoDto updateDto)
    {
        _logger.Debug("Trying action {action} with dto: {dto}", "update", updateDto);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _deslocamentoService.UpdateDeslocamentoAsync(updateDto);
            _logger.Information("action {action} succeeded with dto: {dto}", "updated", updateDto);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..UpdateDeslocamento [HttpPut]


    [HttpDelete("{id}")]
    [Consumes("application/json")]
    public async Task<IActionResult> DeleteDeslocamento(int id)
    {
        _logger.Debug("Trying action {action} for id {id}", "delete", id);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var deleteDto = new DeleteDeslocamentoDto() { Id = id };
            await _deslocamentoService.DeleteDeslocamentoAsync(deleteDto);
            _logger.Information("action {action} succeeded for id {id}", "delete", id);
            return Ok("Entidade removida");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..DeleteDeslocamento [HttpDelete]


    [HttpGet("{id}")]
    [Consumes("application/json")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            _logger.Debug("Trying action {action} for id {id}", "getById", id);
            var dto = new GetByIdDeslocamentoDto() { Id = id };
            var entity = await _deslocamentoService.GetByIdAsync(dto);
            _logger.Information("action {action} succeeded for id {id}", "getById", id);
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
            _logger.Debug("Trying action {action}", "getAll");
            var list = await _deslocamentoService.GetAllAsync();
            _logger.Information("action {action} succeeded with list count: {count}", "getAll", list.Count());
            return Ok(list);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }//..GetAll [HttpGet]



} //..class

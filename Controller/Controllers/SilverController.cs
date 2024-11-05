using BO;
using Microsoft.AspNetCore.Mvc;
using REPO;

namespace Controller.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SilverController : ControllerBase
{
    private readonly ISilverJewelryRepo _silverJewelryRepository;

    public SilverController(ISilverJewelryRepo silverJewelryRepository)
    {
        _silverJewelryRepository = silverJewelryRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var result = await _silverJewelryRepository.GetAllAsync(page, pageSize);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _silverJewelryRepository.GetByIdAsync(id);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _silverJewelryRepository.DeleteAsync(id);
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] SilverJewelry entity)
    {
        if (id != entity.SilverJewelryId)
        {
            return BadRequest();
        }
        await _silverJewelryRepository.UpdateAsync(entity);
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SilverJewelry entity)
    {
        await _silverJewelryRepository.AddAsync(entity);
        return CreatedAtAction("Get", new { id = entity.SilverJewelryId }, entity);
    }



}

using BO;
using Microsoft.AspNetCore.Mvc;
using REPO;

namespace Controller.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryRepo _categoryRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _categoryRepository.GetAllAsync();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _categoryRepository.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Category category)
    {
        await _categoryRepository.AddAsync(category);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Category category)
    {
        await _categoryRepository.UpdateAsync(category);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryRepository.DeleteAsync(id);
        return Ok();
    }
    
}
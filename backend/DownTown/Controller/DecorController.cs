using DownTown.Dto;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class DecorController : ControllerBase
{
    private readonly IDecorService _decorService;

    public DecorController(IDecorService decorService)
    {
        _decorService = decorService;
    }

    [HttpPost("addDecor")]
    public async Task<IActionResult> AddDecor([FromForm] DecorDto decorDto)
    {
        var result = await _decorService.Create(decorDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("updateDecor")]
    public async Task<IActionResult> UpdateDecor([FromForm] DecorDto decorDto)
    {
        var result = await _decorService.Update(decorDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("getAllDecors")]
    public async Task<IActionResult> GetAllDecors()
    {
        var result = await _decorService.GetAll();
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("deleteDecor/{id:int}")]
    public async Task<IActionResult> DeleteDecor(int id)
    {
        var result = await _decorService.Delete(id);
        return StatusCode(result.StatusCode, result);
    }
}

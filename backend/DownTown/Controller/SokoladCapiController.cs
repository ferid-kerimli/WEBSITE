using DownTown.Dto.SokoladCapi;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class SokoladCapiController : ControllerBase
{
    private readonly ISokoladCapiService _service;

    public SokoladCapiController(ISokoladCapiService service)
    {
        _service = service;
    }

    [HttpPost("addSokoladCapi")]
    public async Task<IActionResult> Add([FromForm] SokoladCapiDto dto) =>
        StatusCode((await _service.Create(dto)).StatusCode, await _service.Create(dto));

    [HttpPut("updateSokoladCapi")]
    public async Task<IActionResult> Update([FromForm] SokoladCapiDto dto) =>
        StatusCode((await _service.Update(dto)).StatusCode, await _service.Update(dto));

    [HttpGet("getAllSokoladCapi")]
    public async Task<IActionResult> GetAll() =>
        StatusCode((await _service.Get()).StatusCode, await _service.Get());

    [HttpDelete("deleteSokoladCapi/{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        StatusCode((await _service.Delete(id)).StatusCode, await _service.Delete(id));
}

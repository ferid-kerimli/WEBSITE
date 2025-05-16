using DownTown.Dto.CapIsleri;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CapIsleriController : ControllerBase
{
    private readonly ICapIsleriService _service;

    public CapIsleriController(ICapIsleriService service)
    {
        _service = service;
    }

    [HttpPost("addCapIsleri")]
    public async Task<IActionResult> Add([FromForm] CapIsleriDto dto) =>
        StatusCode((await _service.Create(dto)).StatusCode, await _service.Create(dto));

    [HttpPut("updateCapIsleri")]
    public async Task<IActionResult> Update([FromForm] CapIsleriDto dto) =>
        StatusCode((await _service.Update(dto)).StatusCode, await _service.Update(dto));

    [HttpGet("getAllCapIsleri")]
    public async Task<IActionResult> GetAll() =>
        StatusCode((await _service.Get()).StatusCode, await _service.Get());

    [HttpDelete("deleteCapIsleri/{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        StatusCode((await _service.Delete(id)).StatusCode, await _service.Delete(id));
}
using DownTown.Dto.AcilisTedbirleri;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AcilisTedbirleriController : ControllerBase
{
    private readonly IAcilisTedbirleriService _service;

    public AcilisTedbirleriController(IAcilisTedbirleriService service)
    {
        _service = service;
    }

    [HttpPost("addAcilisTedbirleri")]
    public async Task<IActionResult> Add([FromForm] AcilisTedbirleriDto dto) =>
        StatusCode((await _service.Create(dto)).StatusCode, await _service.Create(dto));

    [HttpPut("updateAcilisTedbirleri")]
    public async Task<IActionResult> Update([FromForm] AcilisTedbirleriDto dto) =>
        StatusCode((await _service.Update(dto)).StatusCode, await _service.Update(dto));

    [HttpGet("getAllAcilisTedbirleri")]
    public async Task<IActionResult> GetAll() =>
        StatusCode((await _service.GetAll()).StatusCode, await _service.GetAll());

    [HttpDelete("deleteAcilisTedbirleri/{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        StatusCode((await _service.Delete(id)).StatusCode, await _service.Delete(id));
}

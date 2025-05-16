using DownTown.Dto.AdGunleri;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdGunleriController : ControllerBase
{
    private readonly IAdGunleriService _service;

    public AdGunleriController(IAdGunleriService service)
    {
        _service = service;
    }

    [HttpPost("addAdGunleri")]
    public async Task<IActionResult> Add([FromForm] AdGunleriDto dto) =>
        StatusCode((await _service.Create(dto)).StatusCode, await _service.Create(dto));

    [HttpPut("updateAdGunleri")]
    public async Task<IActionResult> Update([FromForm] AdGunleriDto dto) =>
        StatusCode((await _service.Update(dto)).StatusCode, await _service.Update(dto));

    [HttpGet("getAllAdGunleri")]
    public async Task<IActionResult> GetAll() =>
        StatusCode((await _service.Get()).StatusCode, await _service.Get());

    [HttpDelete("deleteAdGunleri/{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        StatusCode((await _service.Delete(id)).StatusCode, await _service.Delete(id));
}

using DownTown.Dto.BoyOrGirl;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BoyOrGirlController : ControllerBase
{
    private readonly IBoyOrGirlService _service;

    public BoyOrGirlController(IBoyOrGirlService service)
    {
        _service = service;
    }

    [HttpPost("addBoyOrGirl")]
    public async Task<IActionResult> Add([FromForm] BoyOrGirlDto dto) =>
        StatusCode((await _service.Create(dto)).StatusCode, await _service.Create(dto));

    [HttpPut("updateBoyOrGirl")]
    public async Task<IActionResult> Update([FromForm] BoyOrGirlDto dto) =>
        StatusCode((await _service.Update(dto)).StatusCode, await _service.Update(dto));

    [HttpGet("getAllBoyOrGirl")]
    public async Task<IActionResult> GetAll() =>
        StatusCode((await _service.Get()).StatusCode, await _service.Get());

    [HttpDelete("deleteBoyOrGirl/{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        StatusCode((await _service.Delete(id)).StatusCode, await _service.Delete(id));
}

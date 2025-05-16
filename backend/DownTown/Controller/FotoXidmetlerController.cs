using DownTown.Dto.FotoXidmetler;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Microsoft.AspNetCore.Components.Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class FotoXidmetlerController : ControllerBase
{
    private readonly IFotoXidmetlerService _service;

    public FotoXidmetlerController(IFotoXidmetlerService service)
    {
        _service = service;
    }

    [HttpPost("addFotoXidmetler")]
    public async Task<IActionResult> Add([FromForm] FotoXidmetlerDto dto) =>
        StatusCode((await _service.Create(dto)).StatusCode, await _service.Create(dto));

    [HttpPut("updateFotoXidmetler")]
    public async Task<IActionResult> Update([FromForm] FotoXidmetlerDto dto) =>
        StatusCode((await _service.Update(dto)).StatusCode, await _service.Update(dto));

    [HttpGet("getAllFotoXidmetler")]
    public async Task<IActionResult> GetAll() =>
        StatusCode((await _service.Get()).StatusCode, await _service.Get());

    [HttpDelete("deleteFotoXidmetler/{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        StatusCode((await _service.Delete(id)).StatusCode, await _service.Delete(id));
}

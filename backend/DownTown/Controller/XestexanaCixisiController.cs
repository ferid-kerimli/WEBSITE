using DownTown.Dto.XestexanaCixisi;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class XestexanaCixisiController : ControllerBase
{
    private readonly IXestexanaCixisiService _service;

    public XestexanaCixisiController(IXestexanaCixisiService service)
    {
        _service = service;
    }

    [HttpPost("addXestexanaCixisi")]
    public async Task<IActionResult> Add([FromForm] XestexanaCixisiDto dto) =>
        StatusCode((await _service.Create(dto)).StatusCode, await _service.Create(dto));

    [HttpPut("updateXestexanaCixisi")]
    public async Task<IActionResult> Update([FromForm] XestexanaCixisiDto dto) =>
        StatusCode((await _service.Update(dto)).StatusCode, await _service.Update(dto));

    [HttpGet("getAllXestexanaCixisi")]
    public async Task<IActionResult> GetAll() =>
        StatusCode((await _service.Get()).StatusCode, await _service.Get());

    [HttpDelete("deleteXestexanaCixisi/{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        StatusCode((await _service.Delete(id)).StatusCode, await _service.Delete(id));
}

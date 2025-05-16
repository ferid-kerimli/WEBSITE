using DownTown.Dto.MasaBezedilmesi;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class MasaBezedilmesiController : ControllerBase
{
    private readonly IMasaBezedilmesiService _service;

    public MasaBezedilmesiController(IMasaBezedilmesiService service)
    {
        _service = service;
    }

    [HttpPost("addMasaBezedilmesi")]
    public async Task<IActionResult> Add([FromForm] MasaBezedilmesiDto dto) =>
        StatusCode((await _service.Create(dto)).StatusCode, await _service.Create(dto));

    [HttpPut("updateMasaBezedilmesi")]
    public async Task<IActionResult> Update([FromForm] MasaBezedilmesiDto dto) =>
        StatusCode((await _service.Update(dto)).StatusCode, await _service.Update(dto));

    [HttpGet("getAllMasaBezedilmesi")]
    public async Task<IActionResult> GetAll() =>
        StatusCode((await _service.Get()).StatusCode, await _service.Get());

    [HttpDelete("deleteMasaBezedilmesi/{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        StatusCode((await _service.Delete(id)).StatusCode, await _service.Delete(id));
}

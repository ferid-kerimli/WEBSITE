using DownTown.Dto;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("getImage")]
    public async Task<IActionResult> GetImage(int id)
    {
        var result = await _imageService.GetImage(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("addImage")]
    public async Task<IActionResult> AddImage([FromForm] ImageDto imageDto)
    {
        var result = await _imageService.UploadImage(imageDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("updateImage")]
    public async Task<IActionResult> UpdateImage(int id, [FromForm] ImageDto imageDto)
    {
        var result = await _imageService.UpdateImage(id, imageDto);
        return StatusCode(result.StatusCode, result);
    }
}
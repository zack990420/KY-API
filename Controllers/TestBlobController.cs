using KYAPI.Enums;
using KYAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KYAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestBlobController : ControllerBase
{
    private readonly IBlobStorageService _blobService;
    private readonly ICurrentUserService _currentUserService;

    public TestBlobController(IBlobStorageService blobService, ICurrentUserService currentUserService)
    {
        _blobService = blobService;
        _currentUserService = currentUserService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var stream = file.OpenReadStream();
        var userId = _currentUserService.GetUserId();
        var url = await _blobService.UploadAsync(
            "test-uploads",
            BlobContentType.User,
            file.FileName,
            stream,
            BlobType.Image,
            userId
        );
        return Ok(new { Url = url, UserId = userId });
    }
}

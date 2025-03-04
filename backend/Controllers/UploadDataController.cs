using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DataStorageAPI.Controllers;
[Authorize]
[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase {
    private readonly BlobStorageService _blobService;

    public UploadController(BlobStorageService blobService)
    {
        _blobService = blobService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] UploadRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var fileName = await _blobService.UploadFileAsync(request.File);

        var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
        if (!allowedExtensions.Contains(Path.GetExtension(fileName).ToLower()))
            return BadRequest(new { message = "Only .jpg, .png, and .jpeg files are allowed." });


        var metadata = new {
            FileName = fileName,
            Dropdown = request.Dropdown,
            InputText = request.InputText,
            FreeText = request.FreeText,
            UploadedAt = DateTime.UtcNow
        };


        await _blobService.UploadMetadataAsync(fileName, metadata);

        return Ok(new { message = "File uploaded successfully", 
            fileUrl = $"https://yourbloburl/{fileName}" });
    }
}

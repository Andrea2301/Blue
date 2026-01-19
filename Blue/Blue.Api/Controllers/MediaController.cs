using Blue.Api.Controllers.Requests;
using Blue.Application.Media.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blue.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly UploadMediaCommand _upload;
    private readonly GetAllMediaQuery _getAll;
    private readonly GetMediaByIdQuery _getById;
    private readonly DeleteMediaCommand _delete;
    private readonly UpdateMediaCommand _update;

    public MediaController(
        UploadMediaCommand upload,
        GetAllMediaQuery getAll,
        GetMediaByIdQuery getById,
        DeleteMediaCommand delete,
        UpdateMediaCommand update)
    {
        _upload = upload;
        _getAll = getAll;
        _getById = getById;
        _delete = delete;
        _update = update;
    }

// --------------------
// UPLOAD
// --------------------
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(
        [FromForm] UploadMediaRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest("Archivo no enviado");

        await using var stream = request.File.OpenReadStream();

        var result = await _upload.ExecuteAsync(
            stream,
            request.File.FileName,
            request.File.ContentType);

        return Ok(result);
    }



    // --------------------
    // GET ALL
    // --------------------
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _getAll.ExecuteAsync());

    // --------------------
    // GET BY ID
    // --------------------
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getById.ExecuteAsync(id);
        return result == null ? NotFound() : Ok(result);
    }
// --------------------
// UPDATE
// --------------------
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromForm] UploadMediaRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest("Archivo no enviado");

        await using var stream = request.File.OpenReadStream();

        var result = await _update.ExecuteAsync(
            id,
            stream,
            request.File.FileName,
            request.File.ContentType);

        return Ok(result);
    }


    // --------------------
    // DELETE
    // --------------------
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _delete.ExecuteAsync(id);
        return NoContent();
    }
}

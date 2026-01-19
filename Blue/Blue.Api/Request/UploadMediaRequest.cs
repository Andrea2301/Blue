using Microsoft.AspNetCore.Http;

namespace Blue.Api.Controllers.Requests;

public class UploadMediaRequest
{
    public IFormFile File { get; set; } = default!;
}
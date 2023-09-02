using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            //folder
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            //get file path
            var path = Path.Combine(folder, file?.FileName);

            //stream
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var result = new { file = file.FileName, path = path, size = file.Length };

            return StatusCode(201,result);
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download(string fileName)
        {
            //filePath
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Media", fileName);
            //ContentType: MIME
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(fileName, out var contentType)) 
            {
                contentType = "application/octet-stream";
            }
            //Read
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes,contentType,Path.GetFileName(filePath));
        }
    }
}

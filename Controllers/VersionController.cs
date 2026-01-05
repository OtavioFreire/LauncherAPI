using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LauncherAPI
{
    [ApiController]
    [Route("")]
    public class VersionController : Controller
    {
        private readonly ILogger<VersionController> _logger;
        private readonly VersionSettings _settings;

        public VersionController(IOptions<VersionSettings> options, ILogger<VersionController> logger)
        {
            _logger = logger;
            _settings = options.Value;
        }


        [HttpGet("latest")]
        public IActionResult Latest([FromQuery] string cnpj, string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return BadRequest("Versão não informada");

            LastestResponse lastestResponse = VersionService.Latest(_settings, cnpj, version);

            return Ok(lastestResponse);
        }

        [HttpGet("download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return BadRequest();

            if (fileName.Contains(".."))
                return BadRequest();

            var filePath = Path.Combine(_settings.PackagesPath, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read
            );

            return File(
                stream,
                "application/zip",
                fileName
            );
        }
    }
}
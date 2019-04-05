using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCloudSystem.Bll.Services.Files;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCloudSystem.Bll.Dto.Files;
using Microsoft.AspNetCore.Http;
using WebCloudSystem.Bll.Services.Utils;

namespace WebCloudSystem.Web.Controllers{
    
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService,IParserService parserService): base(parserService) {
            _fileService = fileService;
        }


        [HttpGet("user")]
        public async Task<IActionResult> GetFilesByUser([FromQuery]FileDtoPagedQuery fileQuery) {
            var userId = GetUserIdFromClaim();
            var result = await _fileService.GetFilesByUser(userId,fileQuery);

            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file) {
            var userId = GetUserIdFromClaim();
            var result = await _fileService.Upload(file,userId);
            return Ok(result);
        }

    }
}
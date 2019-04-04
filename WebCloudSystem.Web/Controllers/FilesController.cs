using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCloudSystem.Bll.Services.Files;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCloudSystem.Bll.Dto.Files;

namespace WebCloudSystem.Web.Controllers{
    
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService) {
            _fileService = fileService;
        }


        [HttpGet("user")]
        public async Task<IActionResult> GetFilesByUser([FromQuery]FileDtoPagedQuery fileQuery) {
            var userId = this.User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await _fileService.GetFilesByUser(userId,fileQuery);

            if(result != null) {
                return Ok(result);
            }else {
                return BadRequest(new {message = "No files found, upload some!"});
            }
        }

    }
}
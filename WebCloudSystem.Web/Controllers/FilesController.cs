using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCloudSystem.Web.Controllers{
    
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase {

    }
}
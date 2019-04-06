using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebCloudSystem.Bll.Dto.Users;
using WebCloudSystem.Bll.Services.Users;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCloudSystem.Bll.Services.Utils;

namespace WebCloudSystem.Web.Controllers {
    
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService,IParserService parserService): base(parserService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserDto userParam)
        {
            var user = await _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserDto userParam) {
            var user = await _userService.Register(userParam);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut("activation")]
        public async Task<IActionResult> Activate([FromBody] UserDtoActivation activationUserDetails){
            await _userService.ActivateUser(activationUserDetails);
            return Ok();
        }
    }
}
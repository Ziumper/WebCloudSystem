using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebCloudSystem.Bll.Services.Utils;

namespace WebCloudSystem.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {   
        protected readonly IParserService _parserService;

        public ControllerBase(IParserService parserService) {
            _parserService = parserService;
        }
        protected int GetUserIdFromClaim() {
            string userIdFromClaim = this.User.FindFirst(ClaimTypes.Name)?.Value;
            var userId = _parserService.ParseUserId(userIdFromClaim);
            return userId;
        }
    }
}
using WebCloudSystem.Bll.Services.Base;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using WebCloudSystem.Bll.Dto.Users;

namespace WebCloudSystem.Bll.Services.Users
{

    public class UserService : BaseService, IUserService
    {
        public UserDtoWithoutPassword Authenticate(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserDtoWithoutPassword> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
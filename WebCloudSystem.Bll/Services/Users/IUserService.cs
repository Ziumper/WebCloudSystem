using WebCloudSystem.Bll.Services.Base;
using WebCloudSystem.Bll.Dto.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCloudSystem.Bll.Services.Users {
    public interface IUserService : IBaseService
    {
        Task<UserDtoWithoutPassword> Authenticate(string username, string password);
        Task<UserDtoWithoutPassword> Register(UserDto userParam);
        Task<UserDtoWithoutPassword> ActivateUser(UserDtoActivation activationUserDetails);
        Task ResendActivationCode(string useremail);
    }
}
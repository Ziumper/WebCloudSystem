using WebCloudSystem.Bll.Services.Base;
using WebCloudSystem.Bll.Dto.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCloudSystem.Bll.Services.Users {
    public interface IUserService : IBaseService
    {
        Task<UserDtoWithoutPassword> Authenticate(string username, string password);
        IEnumerable<UserDtoWithoutPassword> GetAll();
        Task<UserDtoWithoutPassword> Register(UserDto userParam);
    }
}
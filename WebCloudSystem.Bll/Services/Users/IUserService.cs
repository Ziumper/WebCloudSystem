using WebCloudSystem.Bll.Services.Base;
using WebCloudSystem.Bll.Dto.Users;
using System.Collections.Generic;

namespace WebCloudSystem.Bll.Services.Users {
    public interface IUserService : IBaseService
    {
        UserDtoWithoutPassword Authenticate(string username, string password);
        IEnumerable<UserDtoWithoutPassword> GetAll();
    }
}
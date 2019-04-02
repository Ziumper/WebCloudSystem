using WebCloudSystem.Dal.Models;
using WebCloudSystem.Dal.Repositories.Base;

namespace WebCloudSystem.Dal.Repositories.Users {

    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public UserRepository(WebCloudSystemContext context) : base(context)
        {
        }
    }

}
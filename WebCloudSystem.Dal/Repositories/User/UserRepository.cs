using WebCloudSystem.Dal.Models;
using WebCloudSystem.Dal.Repositories.Base;

namespace WebCloudSystem.Dal.Repositories {

    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(WebCloudSystemContext context) : base(context)
        {
        }
    }

}
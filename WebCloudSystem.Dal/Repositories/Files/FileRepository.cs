using WebCloudSystem.Dal.Repositories.Base;
using WebCloudSystem.Dal.Models;

namespace WebCloudSystem.Dal.Repositories.Files 
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(WebCloudSystemContext context) : base(context)
        {
        }
    }

}
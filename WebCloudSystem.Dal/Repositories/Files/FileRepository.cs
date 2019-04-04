using WebCloudSystem.Dal.Repositories.Base;
using WebCloudSystem.Dal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WebCloudSystem.Dal.Repositories.Files 
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(WebCloudSystemContext context) : base(context)
        {
        }

    }

}
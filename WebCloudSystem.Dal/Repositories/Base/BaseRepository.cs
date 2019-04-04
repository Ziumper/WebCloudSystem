using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCloudSystem.Dal.Models.Base;
using WebCloudSystem.Dal.Repositories.Base;


namespace WebCloudSystem.Dal.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        protected DbSet<T> table;

        public BaseRepository(WebCloudSystemContext context)
        {
            this.table = context.Set<T>();
        }

        public async Task<T> GetOneByIdAsync(int id)
        {
            var result = await this.table.SingleAsync(entity => entity.Id == id);
            return result;
        }

        
        
    }
}
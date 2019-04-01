using Microsoft.EntityFrameworkCore;
using WebCloudSystem.Dal.Models.Base;

namespace WebCloudSystem.Dal.Repositories.Base {
    public abstract class BaseRepository<T> where T:BaseEntity {

        protected DbSet<T> table; 

        public BaseRepository(WebCloudSystemContext context) {
            this.table = context.Set<T>();
        }
    }
}
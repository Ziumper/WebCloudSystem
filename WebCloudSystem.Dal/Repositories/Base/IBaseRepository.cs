using System.Threading.Tasks;
using WebCloudSystem.Dal.Models.Base;

namespace WebCloudSystem.Dal.Repositories.Base {
    public interface IBaseRepository<T> where T: BaseEntity
    {
        Task<T> GetOneByIdAsync(int id);
    }
}
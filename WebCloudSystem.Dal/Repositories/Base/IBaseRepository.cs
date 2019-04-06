using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebCloudSystem.Dal.Models.Base;

namespace WebCloudSystem.Dal.Repositories.Base {
    public interface IBaseRepository<T> where T: BaseEntity
    {
        Task<T> GetOneByIdAsync(int id);
        Task<PagedEntity<T>> GetAllPagedAsync(int page, int size, int filter, bool order, Expression<Func<T,bool>> predicate);
        Task<T> CreateAsync(T entity);    
        Task SaveAsync();
        Task<T> GetOneByAsync(Expression<Func<T,bool>> predicate);
        T Update(T entity);
        T Delete(T entity);

    }
}
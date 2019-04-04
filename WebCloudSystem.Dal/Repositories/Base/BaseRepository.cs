using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebCloudSystem.Dal.Models.Base;
using WebCloudSystem.Dal.Repositories.Base;


namespace WebCloudSystem.Dal.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        protected DbSet<T> _table;

        public BaseRepository(WebCloudSystemContext context)
        {
            this._table = context.Set<T>();
        }

        public async Task<PagedEntity<T>> GetAllPagedAsync(int page, int size, int filter, bool order, Expression<Func<T, bool>> predicate)
        {
            var pagedEntity = new PagedEntity<T>();
            var skipCount = getSkipCount(page,size);
            var result =  _table.Where(predicate);
        
            pagedEntity.Count = await result.CountAsync(); 
            pagedEntity.Entities = await  result.Skip(skipCount).Take(size).ToListAsync();

            return pagedEntity;
        }

        public async Task<T> GetOneByIdAsync(int id)
        {
            var result = await this._table.SingleAsync(entity => entity.Id == id);
            return result;
        }

        private int getSkipCount(int page,int size){
            var skipCount = (page - 1) * size;
            return skipCount;

        }
        
    }
}
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

        protected readonly DbSet<T> _table;
        protected readonly WebCloudSystemContext _context;

        public BaseRepository(WebCloudSystemContext context)
        {
            _table = context.Set<T>();
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.ModificationDate = DateTime.Now;
            var result = await _table.AddAsync(entity);
            return result.Entity;
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

        public async Task<T> GetOneByAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _table.Where(predicate).SingleOrDefaultAsync();
            return result;
        }

        public async Task<T> GetOneByIdAsync(int id)
        {
            var result = await this._table.SingleAsync(entity => entity.Id == id);
            return result;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            return _table.Update(entity).Entity;
        }

        private int getSkipCount(int page,int size){
            var skipCount = (page - 1) * size;
            return skipCount;
        }
        
    }
}
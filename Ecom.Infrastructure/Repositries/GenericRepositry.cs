using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        private readonly AppDbContext _db;
        public GenericRepositry(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(T entity)
        {
           await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var entity = await _db.Set<T>().FindAsync(id);
            _db.Set<T>().Remove(entity);
           await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
          return await _db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var query = _db.Set<T>().AsQueryable();
            foreach (var item in includes) { 
            
                query = query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
             var entity = await _db.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(int id, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();
            foreach (var item in includes)
            {

                query = query.Include(item);
            }
           var entity = await query.FirstOrDefaultAsync(x=>EF.Property<int>(x , "Id") == id);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
           _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}

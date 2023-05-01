using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IGenericRepository;
using ERP_Domians.RequestParameters;
using Microsoft.EntityFrameworkCore;

namespace BusinesssLogic.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, List<string> includes = null)
        {

            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }



            return await query.FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync(List<string> includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            RequestParam requestParams = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if(requestParams!=null)
            return await query.AsNoTracking().Skip((requestParams.PageNumber-1)*requestParams.PageSize).Take(requestParams.PageSize).ToListAsync();

            else
                return await query.AsNoTracking().ToListAsync();


        }
     
        public async Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> predicate,
            List<string> includes = null, Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null, RequestParam requestParams=null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }


            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }


            if (requestParams!= null)
                return await query.AsNoTracking().Skip((requestParams.PageNumber - 1) * requestParams.PageSize).Take(requestParams.PageSize).ToListAsync();

            else
                return await query.AsNoTracking().ToListAsync();

        }
     
        public async void InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async void InsertRangeAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public  void Delete(T entity)
        {
             _dbSet.Remove(entity);
        }
        public void DeleteRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }
  
    }
}

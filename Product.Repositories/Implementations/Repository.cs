using Microsoft.EntityFrameworkCore;
using Product.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Repositories.Implementations
{
    public class Repository<TKey, TObj, TContext> : IRepository<TKey, TObj, TContext> 
        where TObj : class 
        where TContext : DbContext
    {
        private readonly TContext _context;
        public Repository(TContext context)
        {
            _context = context;
        }

        public async Task<TObj> Create(TObj obj)
        {
            var result = await _context.AddAsync(obj);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<TObj> GetAsync(TKey key)
        {
            var result = await _context.Set<TObj>().FindAsync(key);

            _context.ChangeTracker.Clear();

            return result;
        }

        public async Task<IEnumerable<TObj>> ListAsync()
        {
            return await _context.Set<TObj>().ToListAsync();
        }

        public async Task Update(TObj obj)
        {
            _context.Set<TObj>().Update(obj);

            await _context.SaveChangesAsync();
        }
    }
}

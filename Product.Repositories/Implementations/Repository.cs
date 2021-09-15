using Microsoft.EntityFrameworkCore;
using Product.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Repositories.Implementations
{
    public class Repository<K, T, C> : IRepository<K, T, C> 
        where T : class 
        where C : DbContext
    {
        private readonly C _context;
        public Repository(C context)
        {
            _context = context;
        }

        public async Task<T> Create(T obj)
        {
            var result = await _context.AddAsync(obj);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<T> GetAsync(K key)
        {
            return await _context.Set<T>().FindAsync(key);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}

﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Repositories.Interfaces
{
    public interface IRepository<K, T, C>
    {
        Task<T> Create(T obj);
        Task<T> GetAsync(K key);
        Task<IEnumerable<T>> ListAsync();
        Task Update(T obj);
    }
}

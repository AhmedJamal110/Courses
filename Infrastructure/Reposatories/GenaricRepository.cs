using Entity.Interfaces;
using Entity.Specifications;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : class
    {
        private readonly StoreDbContext _context;

        public GenaricRepository( StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T item)
        {
           await  _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountWithSpecAsync(ISpecification<T> spec)
        {
          return await SpecificationEvalutor<T>.GetQuery(_context.Set<T>(), spec).CountAsync();
        }

        public async Task DeleteAsync(T item)
        {
             _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public async  Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvalutor<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(dynamic id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
           return await SpecificationEvalutor<T>.GetQuery(_context.Set<T>(), spec).FirstOrDefaultAsync();

        }

        public async  Task UpdateAsync(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }
    }
}

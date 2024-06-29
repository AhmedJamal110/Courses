using Entity.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Interfaces
{
    public interface IGenaricRepository<T> where T : class
    {

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(dynamic id);

        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);

        //Specificaton

       Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);


        Task<int> CountWithSpecAsync (ISpecification<T> spec);
    
    }
}

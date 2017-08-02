using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin.Study.Legacy.Persons
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAsync(Predicate<T> filter);

        Task AddAsync(T toAdd);

        Task AddRangeAsync(IEnumerable<T> toAdd);

        Task DeleteAsync(Predicate<T> filter);
    }
}
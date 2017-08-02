using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Owin.Study.Legacy.Persons
{
    internal class DictionaryRepository<T> : IRepository<T>
    {
        private readonly HashSet<T> _store;

        public DictionaryRepository()
        {
            _store = new HashSet<T>();
        }

        public Task AddAsync(T toAdd)
        {
            if (EqualityComparer<T>.Default.Equals(toAdd, default(T))) throw new ArgumentNullException(nameof(toAdd));
            _store.Add(toAdd);
            return Task.CompletedTask;
        }

        public async Task AddRangeAsync(IEnumerable<T> toAdd)
        {
            if (toAdd == null) throw new ArgumentNullException();
            foreach (T item in toAdd)
            {
                await AddAsync(item);
            }
        }

        public Task DeleteAsync(Predicate<T> filter)
        {
            _store.RemoveWhere(filter);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<T>>(_store);
        }

        public Task<IEnumerable<T>> GetAsync(Predicate<T> filter)
        {
            return Task.FromResult<IEnumerable<T>>(_store.Where(item => filter(item)));
        }
    }
}
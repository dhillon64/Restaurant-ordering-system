using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Contracts
{
    public interface IRepositoryBase<T> where T:class
    {
        Task<ICollection<T>> FindAll();

        Task<T> FindById(int Id);

        Task<bool> Update(T entity);

        Task<bool> Delete(T entity);
        Task<bool> Create(T entity);

        Task<bool> Exists(int Id);

        Task<bool> Save();
    }
}

using Restaurant_ordering_system.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Contracts
{
    public interface ICategoryRepository: IRepositoryBase<Category>
    {
        Task<bool> Exists(string Name);

        Task<Category> FindByName(string Name);

    }
}

using BeverageVendingMachine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

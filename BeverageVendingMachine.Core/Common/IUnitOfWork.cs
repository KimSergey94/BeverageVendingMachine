using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Common
{
    /// <summary>
    /// Interface for implementation of the pattern Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets needed entity repository and/or initiates its instance
        /// </summary>
        /// <typeparam name="TEntity">TEntity generic type for entity repository</typeparam>
        /// <returns>Returns repository for the provided param type TEntity</returns>
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Commits latest changes to a database
        /// </summary>
        /// <returns>The task result contains the number of state entries written to the database</returns>
        Task<int> Complete();
    }
}
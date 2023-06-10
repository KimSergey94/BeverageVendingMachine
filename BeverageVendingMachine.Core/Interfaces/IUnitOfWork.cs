using BeverageVendingMachine.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces
{
    /// <summary>
    /// Interface for implementation of the pattern Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Method to get needed entity repository
        /// </summary>
        /// <typeparam name="TEntity">TEntity generic type for entity repository</typeparam>
        /// <returns></returns>
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Commits latest changes to a database
        /// </summary>
        /// <returns>The task result contains the number of state entries written to the database</returns>
        Task<int> Complete();
    }
}
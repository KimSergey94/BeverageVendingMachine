using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Infrastructure.Data
{
    /// <summary>
    /// Interface for implementation of the pattern Unit of Work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BeverageVendingMachineContext _beverageVendingMachineContext;
        private Hashtable _repositories;

        public UnitOfWork(BeverageVendingMachineContext beverageVendingMachineContext)
        {
            _beverageVendingMachineContext = beverageVendingMachineContext;
        }

        
        /// <summary>
        /// Disposes db context
        /// </summary>
        public void Dispose()
        {
            _beverageVendingMachineContext.Dispose();
        }

        /// <summary>
        /// Commits latest changes to a database
        /// </summary>
        /// <returns>The task result contains the number of state entries written to the database</returns>
        public async Task<int> Complete()
        {
            return await _beverageVendingMachineContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets needed entity repository and/or initiates its instance
        /// </summary>
        /// <typeparam name="TEntity">TEntity generic type for entity repository</typeparam>
        /// <returns>Returns repository for the provided param type TEntity</returns>
        public IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();
            var Type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)), _beverageVendingMachineContext);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[Type];
        }
    }
}

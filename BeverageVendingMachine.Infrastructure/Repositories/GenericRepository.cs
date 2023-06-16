using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Infrastructure.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly BeverageVendingMachineContext _beverageVendingMachineContext;

        public GenericRepository(BeverageVendingMachineContext beverageVendingMachineContext)
        {
            _beverageVendingMachineContext = beverageVendingMachineContext;
        }


        /// <summary>
        /// Get all generic objects of type T asynchronously
        /// </summary>
        /// <returns>Returns all generic objects of type T</returns>
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _beverageVendingMachineContext.Set<T>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get generic object of type T by Id asynchronously
        /// </summary>
        /// <param name="id">Id of object needed to be returned</param>
        /// <returns>Returns object with specified Id if found</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _beverageVendingMachineContext.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds the passed T type object to a collection asynchronously
        /// </summary>
        /// <param name="entity">T type object to be added to a collection</param>
        /// <returns>Returns added T type object to a collection</returns>
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _beverageVendingMachineContext.AddAsync<T>(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes passed T type object from a collection asynchronously
        /// </summary>
        /// <param name="entity">T type object to be deleted from a collection</param>
        /// <returns>Returns whether T type object has been successfully deleted from a collection</returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                await Task.Run(() => { _beverageVendingMachineContext.Set<T>().Remove(entity); });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates passed T type object in a collection asynchronously
        /// </summary>
        /// <param name="entity">T type object to be updated in a collection</param>
        /// <returns>Returns updated T object</returns>
        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                return await Task.Run(() => { Update(entity); return entity; });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Adds the passed T type object to a collection
        /// </summary>
        /// <param name="entity">T type object to be added to a collection</param>
        public void Add(T entity)
        {
            _beverageVendingMachineContext.Add<T>(entity);
        }

        /// <summary>
        /// Updates passed T type object in a collection
        /// </summary>
        /// <param name="entity">T type object to be updated in a collection</param>
        public void Update(T entity)
        {
            _beverageVendingMachineContext.Attach<T>(entity);
            _beverageVendingMachineContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes passed T type object from a collection
        /// </summary>
        /// <param name="entity">T type object to be deleted from a collection</param>
        public void Delete(T entity)
        {
            _beverageVendingMachineContext.Set<T>().Remove(entity);
        }
    }
}

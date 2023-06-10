using BeverageVendingMachine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
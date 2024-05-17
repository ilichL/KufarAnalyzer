using KufarAnalyzer.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.DataAccess.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task Add(T entity);

        Task<T> GetById(Guid id);

        Task Remove(Guid id);

        Task AddRange(IEnumerable<T> entity);

        IQueryable<T> Get();
    }
}

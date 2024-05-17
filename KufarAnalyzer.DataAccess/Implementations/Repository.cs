using KufarAnalyzer.Data;
using KufarAnalyzer.Data.Abstractions;
using KufarAnalyzer.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KufarAnalyzer.DataAccess.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly KufarContext Context;

        private readonly DbSet<T> Dbset;


        public Repository(KufarContext Context)
        {
            this.Context = Context;
            Dbset = Context.Set<T>();
        }


        public virtual IQueryable<T> Get()
        {
            return Dbset;
        }


        public async Task Add(T entity)
        {
            await Dbset.AddAsync(entity);
        }


        public virtual async Task<T> GetById(Guid id)
        {
            return await Dbset.AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }


        public virtual async Task AddRange(IEnumerable<T> entity)
        {
            await Dbset.AddRangeAsync(entity);
        }


        public virtual async Task Remove(Guid id)
        {
            Dbset.Remove(await Dbset.FindAsync(id));
        }
    }
}

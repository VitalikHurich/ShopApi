using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Context;

namespace ShopApi.DAL.Repositories.RepositoriesInterfaces
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly ShopContext context;
        DbSet<T> dbset;
        public GenericRepository(ShopContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }
        public async Task AddASync(T entity)
        {
            await dbset.AddAsync(entity);
        }

        public async Task<T> FindByIDAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await dbset.ToListAsync();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void Update(T entity)
        {
            dbset.Update(entity);
        }
    }
}
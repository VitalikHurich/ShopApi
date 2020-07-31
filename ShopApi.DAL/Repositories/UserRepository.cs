using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Context;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        ShopContext context;
        DbSet<User> dbset;
        public UserRepository(ShopContext context)
        {
            this.context = context;
            dbset = context.Set<User>();
        }
        public async Task AddASync(User user)
        {
            await dbset.AddAsync(user);
        }

        public async Task<User> FindByIDAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).Include(x => x.Orders).ThenInclude(x => x.User).ToListAsync();
        }

        public void Remove(User user)
        {
            dbset.Remove(user);
        }

        public void Update(User user)
        {
            dbset.Update(user);
        }
    }
}
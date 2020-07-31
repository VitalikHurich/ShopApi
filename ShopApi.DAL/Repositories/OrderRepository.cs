using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Context;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        ShopContext context;
        DbSet<Order> dbset;
        public OrderRepository(ShopContext context)
        {
            this.context = context;
            dbset = context.Set<Order>();
        }
        public async Task AddASync(Order order)
        {
            await dbset.AddAsync(order);
        }

        public async Task<Order> FindByIDAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await context.Orders.Include(x => x.OrderDetails)
                                        .ThenInclude(x => x.Order)
                                        .Include(x => x.OrderDetails)
                                        .ThenInclude(x => x.Good)
                                        .Include(x => x.User)
                                        .ToListAsync();
        }

        public void Remove(Order order)
        {
            dbset.Remove(order);
        }

        public void Update(Order order)
        {
            dbset.Update(order);
        }
    }
}
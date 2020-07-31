using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Context;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.DAL.Repositories
{
    public class OrderDetailRepository : IRepository<OrderDetail>
    {
        ShopContext context;
        DbSet<OrderDetail> dbset;
        public OrderDetailRepository(ShopContext context)
        {
            this.context = context;
            dbset = context.Set<OrderDetail>();
        }

        public async Task AddASync(OrderDetail orderDetail)
        {
            await dbset.AddAsync(orderDetail);
        }

        public async Task<OrderDetail> FindByIDAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<IEnumerable<OrderDetail>> ListAsync()
        {
            return await context.OrderDetails.Include(x => x.Good).Include(x => x.Order).ToListAsync();
        }
        public void Remove(OrderDetail orderDetail)
        {
            dbset.Remove(orderDetail);
        }

        public void Update(OrderDetail orderDetail)
        {
            dbset.Update(orderDetail);
        }
    }
}
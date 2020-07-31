using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.Core.Domain;
using ShopApi.DAL.Context;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.DAL.Repositories
{
    public class GoodRepository : IGoodRepository
    {
        ShopContext context;
        DbSet<Good> dbset;
        public GoodRepository(ShopContext context)
        {
            this.context = context;
            dbset = context.Set<Good>();
        }   
        public async Task AddASync(Good good)
        {
            await dbset.AddAsync(good);
        }

        public async Task<Good> FindByIDAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<PagedList<Good>> ListAsync(GoodParams goodParams)
        {
            var query = context.Goods.Include(x => x.Manufacturer).Include(x => x.Category).AsQueryable();

            if (!string.IsNullOrEmpty(goodParams.OrderBy))
            {
                switch (goodParams.OrderBy)
                {
                    case "low":
                        query = query.OrderByDescending(p => p.GoodName);
                        break;
                    default:
                        query = query.OrderBy(p => p.GoodName);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(goodParams.CategoryName))
                query = query.Where(p=>p.Category.CategoryName == goodParams.CategoryName);

            return await PagedList<Good>.CreateAsync(query, goodParams.PageNumber, goodParams.PageSize);
        }
        public void Remove(Good good)
        {
            dbset.Remove(good);
        }

        public void Update(Good good)
        {
            dbset.Update(good);
        }
    }
}
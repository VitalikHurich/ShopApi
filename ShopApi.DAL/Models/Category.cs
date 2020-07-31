using System.Collections.Generic;

namespace ShopApi.DAL.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IList<Good> Goods { get; set; } = new List<Good>();
    }
}
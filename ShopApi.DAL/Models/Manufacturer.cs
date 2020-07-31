using System.Collections.Generic;

namespace ShopApi.DAL.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public IList<Good> Goods { get; set; } = new List<Good>();
    }
}
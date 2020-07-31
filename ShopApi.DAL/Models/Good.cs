using System.Collections.Generic;

namespace ShopApi.DAL.Models
{
    public class Good
    {
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public int GoodCount { get; set; }
        public bool Available { get; set; }
        public decimal GoodPriceMinimal { get; set; }
        public decimal GoodPriceActual { get; set; }
        public string GoodImageURL { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
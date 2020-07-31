using System.Collections.Generic;

namespace ShopApi.DAL.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int GoodId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public Good Good { get; set; }
        public Order Order { get; set; }
    }
}
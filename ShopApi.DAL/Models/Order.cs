using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.DAL.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeOfCrestion { get; set;}
        public User User { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
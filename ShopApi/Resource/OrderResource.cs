using System;

namespace ShopApi.Resource
{
    public class OrderResource
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeOfCrestion { get; set; }
        public string[] Goods { get; set; }
    }
}
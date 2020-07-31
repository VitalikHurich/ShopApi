using System;

namespace ShopApi.BLL.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeOfCrestion { get; set; }
        public string[] Goods { get; set; }
    }
}
using System;

namespace ShopApi.Resource
{
    public class SaveOrderResource
    {
        public int UserId { get; set; }
        public DateTime TimeOfCrestion { get; set; }
        public string[] Goods { get; set; }
    }
}
namespace ShopApi.Resource
{
    public class OrderDetailResource
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
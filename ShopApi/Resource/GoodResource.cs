namespace ShopApi.Resource
{
    public class GoodResource
    {
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public int GoodCount { get; set; }
        public bool Available { get; set; }
        public decimal GoodPriceActual { get; set; }
        public string GoodImageURL { get; set; }
        public string Description { get; set;}
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    }
}
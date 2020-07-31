namespace ShopApi.Resource
{
    public class SaveGoodResource
    {
        public string GoodName { get; set; }
        public int GoodCount { get; set; }
        public bool Available { get; set; }
        public decimal GoodPriceMinimal { get; set; }
        public decimal GoodPriceActual { get; set; }
        public string GoodImageURL { get; set; }
        public string Description { get; set;}
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
    }
}
namespace ShopApi.BLL.DTO
{
    public class GoodDTO
    {
        private bool available;
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public int GoodCount { get; set; }
        public bool Available
        {
            get
            {
                if(this.GoodCount == 0 || this.GoodPriceActual < this.GoodPriceMinimal)
                {
                    this.available = false;
                    return this.available;
                }
                else
                {
                    this.available = true;
                    return this.available;
                }
            }
            set
            {
                this.available = value;
            }
        }
        public decimal GoodPriceMinimal { get; set; }
        public decimal GoodPriceActual { get; set; }
        public string GoodImageURL { get; set; }
        public string Description { get; set;}
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    }
}
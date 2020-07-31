namespace ShopApi.BLL.DTO
{
    public class OrderDetailDTO
    {
        private decimal totalPrice;
        public int OrderDetailId { get; set; }
        public int OrderId { get; set;}
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice
        {
            get
            {
                if(this.Count > 0)
                {
                    this.totalPrice = this.UnitPrice * this.Count;
                    return totalPrice;
                }
                else
                {
                    return totalPrice = 0;
                }
            }
            set
            {
                this.totalPrice = value;
            }
        }
    }
}
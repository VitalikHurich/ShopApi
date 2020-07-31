using ShopApi.DAL.Models;

namespace ShopApi.BLL.Response
{
    public class GoodResponse : BaseResponse
    {
        public Good Good { get; private set; }
        public GoodResponse(bool success, string message, Good good) : base(success, message)
        {
            Good = good;
        }
        public GoodResponse(Good good) : this(true, string.Empty, good) { }
        public GoodResponse(string message) : this(false, message, null) { }
    }
}
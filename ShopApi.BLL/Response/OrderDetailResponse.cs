using ShopApi.DAL.Models;

namespace ShopApi.BLL.Response
{
    public class OrderDetailResponse : BaseResponse
    {
        public OrderDetail OrderDetail { get; private set; }
        public OrderDetailResponse(bool success, string message, OrderDetail orderDetail) : base(success, message)
        {
            OrderDetail = orderDetail;
        }
        public OrderDetailResponse(OrderDetail orderDetail) : this(true, string.Empty, orderDetail) { }
        public OrderDetailResponse(string message) : this(false, message, null) { }
    }
}
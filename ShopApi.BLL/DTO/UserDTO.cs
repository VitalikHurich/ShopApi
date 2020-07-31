namespace ShopApi.BLL.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public string[] Role { get; set; }
        public string[] OrderList { get; set; }
    }
}
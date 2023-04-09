using ASM_CSharp4_Linhtnph20247.Models;
using Newtonsoft.Json;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public static class SessionService
    {
        //Đưa dữ liệu vào Session dưới dạng Json data
        public static void SetObjToJson(ISession session, string key, object value)
        {
            //Thêm một JsonSerializerSettings mới và thiết lập thuộc tính PreserveReferencesHandling thành Preserve để giữ nguyên reference đến các đối tượng khác, sau đó cấu hình thêm thuộc tính ReferenceLoopHandling thành Ignore để loại bỏ các reference loop

            // Thiết lập JsonSerializerSettings
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            //Obj value là dữ liệu bạn muốn thêm vào Session
            //Chuyển đổi dữ liệu đó sang dạng JsonString
            var jsonString = JsonConvert.SerializeObject(value, settings);
            session.SetString(key, jsonString);
        }

        //Lấy và đưa dữ liệu từ Sesstion về dạng List<Obj>
        public static List<CartDetail> GetObjFromSession(ISession session, string key)
        {
            var data = session.GetString(key); //Đọc dữ liệu từ Session ở dạng chuỗi
            if (data != null)
            {
                var lstObj = JsonConvert.DeserializeObject<List<CartDetail>>(data);
                return lstObj;
            }
            else return new List<CartDetail>();
        }
        public static bool CheckProductInCart(Guid id, List<CartDetail> cartProducts)
        {
            return cartProducts.Any(x => x.ProductId == id); //Kiểm tra xem có tồn tại sản phẩm đó trong Giỏ Hàng chưa
        }
    }
}   

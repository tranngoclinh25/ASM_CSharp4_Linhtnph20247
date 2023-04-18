using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public class UserService : IUserService
    {
        ShopDbContext _shopDbContext;

        public UserService()
        {
            _shopDbContext = new ShopDbContext();
        }
        public List<User> GetAllUsers()
        {
            return _shopDbContext.Users.ToList();
        }
    }
}

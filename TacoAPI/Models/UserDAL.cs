using Microsoft.EntityFrameworkCore;
using TacoAPI;

namespace TacoAPI.Models
{
    public class UserDAL
    {
        static FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();
        public static bool ValidateKey(string ApiKey)
        {

            return dbContext.Users.Any(u => u.ApiKey == ApiKey);

        }
    }
}

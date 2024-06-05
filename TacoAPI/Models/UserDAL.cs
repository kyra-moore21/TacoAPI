using Microsoft.EntityFrameworkCore;
using TacoAPI;

namespace TacoAPI.Models
{
    public class UserDAL
    {
       
        public static bool ValidateKey(string ApiKey)
        {

            FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();
         
            return dbContext.Users.Any(u => u.ApiKey == ApiKey);
                 
        }
    }
}

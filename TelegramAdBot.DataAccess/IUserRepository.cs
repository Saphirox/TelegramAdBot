using System.Threading.Tasks;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess
{
    public interface IUserRepository : IMongoDbRepository<AppUser>
    {
        Task<bool> ExistsByTelegramIdAsync(int telegramId);
        
        Task<AppUser> GetUserByTelegramIdAsync(int telegramId);
    }
}
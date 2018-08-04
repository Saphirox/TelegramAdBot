using System.Threading.Tasks;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess
{
    public interface IUserRepository : IMongoDbRepository<User>
    {
        Task<bool> ExistsByTelegramIdAsync(int telegramId);
    }
}
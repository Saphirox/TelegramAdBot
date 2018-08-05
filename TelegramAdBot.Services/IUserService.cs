using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.Entities;
using TelegramAdBot.Entities.Enums;

namespace TelegramAdBot.Services
{
    public interface IUserService
    {
        Task<bool> ExistsByTelegramIdAsync(int telegramId);
        
        Task<AppUser> CreateUser(User user, UserRole role);
    }
}
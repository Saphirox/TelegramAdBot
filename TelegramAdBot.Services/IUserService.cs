using System.Threading.Tasks;

namespace TelegramAdBot.Services
{
    public interface IUserService
    {
        Task<bool> ExistsByTelegramIdAsync(int telegramId);
    }
}
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramAdBot.Services
{
    public interface IMessageService
    {
        Task ExecuteAsync(Update message);
    }
}
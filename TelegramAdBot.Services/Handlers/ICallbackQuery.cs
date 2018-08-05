using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramAdBot.Services.Handlers
{
    public interface ICallbackQuery
    {
        Task HandleCallbackAsync(CallbackQuery update);
    }
}
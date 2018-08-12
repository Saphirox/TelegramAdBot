using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramAdBot.Services.Handlers
{
    public interface ICallbackQuery
    {
        bool IsAppropriate(CallbackQuery query);
        
        Task HandleCallbackAsync(CallbackQuery cquery);
    }
}
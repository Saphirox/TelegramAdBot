using Telegram.Bot;

namespace TelegramAdBot.Services
{
    public interface IBotService
    {
        TelegramBotClient Client { get; set; }
    }
}
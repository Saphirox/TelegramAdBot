using Telegram.Bot.Types;

namespace TelegramAdBot.Services.Handlers
{
    public interface ICommand
    {
        string CommandName { get; }
        
        bool RequireAuthentication { get; }
        
        void HandleMessage(Update update);
    }
}
using Telegram.Bot.Types;

namespace TelegramAdBot.Services.Handlers
{
    public interface ICommand
    {
        string CommandName { get; }
        
        void HandleMessage(Message message);
    }
}
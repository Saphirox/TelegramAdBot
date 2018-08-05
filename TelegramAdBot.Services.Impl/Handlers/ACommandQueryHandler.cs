using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.Services.Handlers;

namespace TelegramAdBot.Services.Impl.Handlers
{
    public abstract class ACommandQueryHandler : ICallbackQuery
    {
        private readonly IBotService _bot;
        
        protected ACommandQueryHandler(IBotService bot)
        {
            _bot = bot;
        }
        
        protected abstract string Text { get; }

        protected abstract void Callback(CallbackQuery callbackQuery);
      
        public async Task HandleCallbackAsync(CallbackQuery update)
        {
            Callback(update);
            
            await _bot.Client.SendTextMessageAsync(update.Message.Chat.Id, Text);
        }
    }
}
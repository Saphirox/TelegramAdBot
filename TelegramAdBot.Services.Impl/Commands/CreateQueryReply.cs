using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramAdBot.DataAccess;
using TelegramAdBot.Entities;
using TelegramAdBot.Services.Handlers;
using TelegramAdBot.Services.Impl.Helpers;

namespace TelegramAdBot.Services.Impl.Commands
{
    public class CreateQueryReply : ICommand
    {
        private readonly IBotService _bot;
        
        public CreateQueryReply(IBotService bot)
        {
            _bot = bot;
        }
        
        public string CommandName => "/create-query";

        public bool RequireAuthentication => true;
        
        public async void HandleMessage(Update update)
        {
            var message = "Enter query name for promotion";

            await _bot.Client.SendTextMessageAsync(update.Message.Chat.Id, message, replyMarkup: new ForceReplyMarkup());
        }
    }
}
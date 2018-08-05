using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramAdBot.Services.Handlers;

namespace TelegramAdBot.Services.Impl.Commands
{
    public abstract class AKeyboardCommand : ICommand
    {
        private IBotService _bot;
        
        public AKeyboardCommand(IBotService bot)
        {
            _bot = bot;
        }
        
        public abstract string CommandName { get; }
        
        public abstract bool RequireAuthentication { get; }

        protected virtual string Text => string.Empty;

        public async void HandleMessage(Update update)
        {
            var keyboard = HandleKeyboard();
                        
            await _bot.Client.SendTextMessageAsync(update.Message.Chat.Id, Text, ParseMode.Default, false, false, 0, keyboard);

            Callback(update);
        }

        protected abstract IReplyMarkup HandleKeyboard();

        protected virtual void Callback(Update update)
        {
        }
    }
}
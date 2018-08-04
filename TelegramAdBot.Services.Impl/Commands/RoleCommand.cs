using System;
using Telegram.Bot.Types;
using TelegramAdBot.Services.Handlers;

namespace TelegramAdBot.Services.Impl.Commands
{
    public abstract class KeyboardCommand : ICommand
    {
        public string CommandName { get; } = "default";

        public virtual void HandleMessage(Message message)
        {
            
        }
    }
}
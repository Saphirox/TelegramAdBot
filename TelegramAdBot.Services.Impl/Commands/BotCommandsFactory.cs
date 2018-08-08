using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramAdBot.Services.Handlers;

namespace TelegramAdBot.Services.Impl.Commands
{
    public class BotCommandsFactory : IBotCommandsFactory
    {
        private readonly Lazy<IEnumerable<ICommand>> _commands;
        private readonly Lazy<IEnumerable<ICallbackQuery>> _handlers;
        private readonly Lazy<IEnumerable<IReplyCommand>> _replyCommands;
        
        public BotCommandsFactory(Lazy<IEnumerable<ICommand>> commands, Lazy<IEnumerable<ICallbackQuery>> handlers, Lazy<IEnumerable<IReplyCommand>> replyCommands)
        {
            _commands = commands;
            _handlers = handlers;
            _replyCommands = replyCommands;
        }
        
        public IEnumerable<ICommand> GetCommands()
        {
            return _commands.Value;
        }
        
        public IEnumerable<ICallbackQuery> GetCallbacks()
        {
            return _handlers.Value;
        }

        public IEnumerable<string> GetCommandNames()
        {
            return _commands.Value.Select(c => c.CommandName);
        }

        public string GetCommandNameByType(string className)
        {
            return _commands.Value.FirstOrDefault(x => x.GetType().Name == className)?.CommandName;
        }

        public IEnumerable<IReplyCommand> GetReplyCommands()
        {
            return _replyCommands.Value;
        }
    }
}
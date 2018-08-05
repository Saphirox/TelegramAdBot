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
        
        public BotCommandsFactory(Lazy<IEnumerable<ICommand>> commands, Lazy<IEnumerable<ICallbackQuery>> handlers)
        {
            _commands = commands;
            _handlers = handlers;
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
        
    }
}
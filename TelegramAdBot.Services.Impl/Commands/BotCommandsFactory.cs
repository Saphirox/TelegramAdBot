using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramAdBot.Services.Handlers;

namespace TelegramAdBot.Services.Impl.Commands
{
    public class BotCommandsFactory : IBotCommandsFactory
    {
        private readonly ICollection<ICommand> _commands;
        
        public BotCommandsFactory()
        {
           ICollection<ICommand> commands = 
               Assembly.GetExecutingAssembly().GetTypes()
                   .Select(c => c.IsAssignableFrom(typeof(ICommand)))
                   .Select(c => Activator.CreateInstance(c.GetType()))
                   .Cast<ICommand>()
                   .ToList();

            _commands = commands;
        }
        
        public ICollection<ICommand> GetCommands()
        {
            return _commands;
        }
    }
}
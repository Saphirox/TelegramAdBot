using System.Collections.Generic;
using TelegramAdBot.Services.Handlers;

namespace TelegramAdBot.Services
{
    public interface IBotCommandsFactory
    {
        ICollection<ICommand> GetCommands();
    }
}
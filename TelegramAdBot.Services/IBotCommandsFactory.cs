using System.Collections.Generic;
using TelegramAdBot.Services.Handlers;

namespace TelegramAdBot.Services
{
    public interface IBotCommandsFactory
    {
        IEnumerable<ICommand> GetCommands();

        IEnumerable<string> GetCommandNames();

        IEnumerable<ICallbackQuery> GetCallbacks();

        string GetCommandNameByType(string className);
    }
}
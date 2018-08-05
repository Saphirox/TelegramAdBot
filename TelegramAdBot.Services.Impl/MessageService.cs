using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.Entities.Enums;
using TelegramAdBot.Services.Handlers;

namespace TelegramAdBot.Services.Impl
{
    public class MessageService : IMessageService
    {
        private readonly IBotCommandsFactory _botCommandsFactory;
        private readonly IUserService _userService;
        private readonly IBotService _bot;
        
        public MessageService(IBotCommandsFactory botCommandsFactory, IUserService userService, IBotService bot)
        {
            _botCommandsFactory = botCommandsFactory;
            _userService = userService;
            _bot = bot;
        }
        
        public async Task ExecuteAsync(Update update)
        {
            if (update == null)
            {
                return;
            }

            if (update.Message != null)
            {
                var userExist = await _userService.ExistsByTelegramIdAsync(update.Message.From.Id);
                var commands = _botCommandsFactory.GetCommands();
            
                foreach (ICommand command in commands)
                {
                    if (command.CommandName == update.Message.Text && command.RequireAuthentication == userExist)
                    {
                        command.HandleMessage(update);
                        return;
                    }
                }
            } 
            else if (update.CallbackQuery != null)
            {
                var callbacks = _botCommandsFactory.GetCallbacks();

                // TODO: FIX me
                foreach (var callback in callbacks)
                {
                    if (Enum.TryParse(typeof(UserRole), update.CallbackQuery.Data, out var _))
                    {
                        await callback.HandleCallbackAsync(update.CallbackQuery);
                        return;
                    }
                }
            }
            else
            {
                //await _bot.Client.SendTextMessageAsync(update.Message.Chat.Id, "Wrong command");
            }
        }
    }
}
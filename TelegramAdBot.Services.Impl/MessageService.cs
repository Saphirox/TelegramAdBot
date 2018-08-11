using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.Services.Handlers;
using TelegramAdBot.Services.Impl.Helpers;

namespace TelegramAdBot.Services.Impl
{
    public class MessageService : IMessageService
    {
        private readonly IBotCommandsFactory _botCommandsFactory;
        private readonly IUserService _userService;
        
        public MessageService(IBotCommandsFactory botCommandsFactory, IUserService userService)
        {
            _botCommandsFactory = botCommandsFactory;
            _userService = userService;
        }
        
        // TODO: -> Need to refactor
        public async Task ExecuteAsync(Update update)
        {
            if (update == null)
            {
                return;
            }

            if (update.Message != null)
            {
                CurrentUser.SetInstance(await _userService.GetUserByTelegramIdAsync(update.Message.From.Id));
                
                var commands = _botCommandsFactory.GetCommands();
            
                foreach (ICommand command in commands)
                {
                    if (command.CommandName == update.Message.Text && command.RequireAuthentication == CurrentUser.Exists())
                    {
                        // TODO: Create interface as async
                        command.HandleMessage(update);
                        return;
                    }
                }

                if (CurrentUser.Exists())
                {
                    var replyCommands = _botCommandsFactory.GetReplyCommands();
                    
                    foreach (var replyCommand in replyCommands)
                    {
                        if (replyCommand.IsAppropriate(update.Message))
                        {
                            await replyCommand.HandleReply(update.Message);
                        }
                    }
                }
            } 
            else if (update.CallbackQuery != null)
            {
                var callbacks = _botCommandsFactory.GetCallbacks();

                foreach (ICallbackQuery callback in callbacks)
                {
                    if (callback.IsAppropriate(update.CallbackQuery))
                    {
                        await callback.HandleCallbackAsync(update.CallbackQuery);
                    }
                }
            }
            else
            {
                // TODO: Add own logic
            }
        }
    }
}
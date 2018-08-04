using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
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
            var userExist = await _userService.ExistsByTelegramIdAsync(update.Message.From.Id);
            
            if (userExist)
            {
                var commands = _botCommandsFactory.GetCommands();

                foreach (ICommand command in commands)
                {
                    command.HandleMessage(update.Message);
                }
            }
            else
            {
                var rkm = new ReplyKeyboardMarkup();
             
                rkm.Keyboard = new IEnumerable<KeyboardButton>[]
                {
                    new KeyboardButton[]
                    {
                        new KeyboardButton()
                        {
                            Text = "I wanna promote my product",
                        },
                        new KeyboardButton()
                        {
                            Text = "I post someone ads",
                        },
                    },
                };
                
                await _bot.Client.SendTextMessageAsync(update.Message.Chat.Id, 
                    "Choose one option", ParseMode.Default, false, false, 0, rkm);
            }
        }
    }
}
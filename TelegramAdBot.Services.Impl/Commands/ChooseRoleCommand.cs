using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramAdBot.Entities.Enums;

namespace TelegramAdBot.Services.Impl.Commands
{
    public class ChooseRoleCommand : AKeyboardCommand
    {
        private IUserService _userService;
        
        public ChooseRoleCommand(IBotService bot, IUserService userService) : base(bot)
        {
            _userService = userService;
        }

        public override string CommandName => "/start";

        protected override string Text => "keyboard";

        public override bool RequireAuthentication => false;
        
        protected override IReplyMarkup HandleKeyboard(Update update)
        {
            var buttons = new[]
            {
                new InlineKeyboardButton
                {
                    Text = "I wanna promote my product",
                    CallbackData = UserRole.PromoteAd.ToString()
                },
                new InlineKeyboardButton
                {
                    Text = "I post someone ads",
                    CallbackData = UserRole.PostADonMyPage.ToString()
                }
            };
            
            var inlineKeyboard = new InlineKeyboardMarkup(buttons);

            return inlineKeyboard;
        }
    }
}
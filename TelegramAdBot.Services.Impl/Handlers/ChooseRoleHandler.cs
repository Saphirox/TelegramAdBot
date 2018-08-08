using System;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using TelegramAdBot.Entities.Enums;

namespace TelegramAdBot.Services.Impl.Handlers
{
    public class ChooseRoleHandler : ACommandQueryHandler
    {
        private readonly IUserService _userService;
        private readonly ILogger<ChooseRoleHandler> _logger;
        private string _text;

        public ChooseRoleHandler(IBotService bot, IUserService userService, ILogger<ChooseRoleHandler> logger) : base(bot)
        {
            _userService = userService;
            _logger = logger;
        }

        protected override string Text => _text;
        
        protected override async void Callback(CallbackQuery callbackQuery)
        {
            var data = Enum.Parse<UserRole>(callbackQuery.Data);

            try
            {
                var userExist = await _userService.ExistsByTelegramIdAsync(callbackQuery.From.Id);
            
                if (!userExist)
                {
                    await _userService.CreateUser(callbackQuery.From, data);
                }

                _text = "Great, you choose a role";
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message, e);

                _text = "Internal server error";
            }
        }

        public override bool IsAppropriate(CallbackQuery query)
        {
            return Enum.TryParse(typeof(UserRole), query.Data, out var _);
        }
    }
}
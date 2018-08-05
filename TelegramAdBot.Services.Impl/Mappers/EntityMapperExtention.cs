
using TelegramAdBot.Entities;
using TelegramAdBot.Entities.Enums;

namespace TelegramAdBot.Services.Impl.Mappers
{
    using TelegramUser = Telegram.Bot.Types.User;
    
    public static class EntityMapperExtention
    {
        public static AppUser ToEntity(this TelegramUser user, UserRole role)
        {
            return new AppUser
            {
                FirstName =  user.FirstName,
                LastName = user.LastName,
                IsBot = user.IsBot,
                LanguageCode = user.LanguageCode,
                TelegramId  = user.Id,
                UserRole = role,
                UserName = user.Username
            };
        }
    }
}
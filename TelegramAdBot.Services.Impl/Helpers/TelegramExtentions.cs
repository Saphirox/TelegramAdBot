using Telegram.Bot.Types;

namespace TelegramAdBot.Services.Impl.Helpers
{
    public static class TelegramExtentions
    {
        public static long CurrentChatId(this Message message)
        {
            return message.Chat.Id;
        }

        public static long CurrentChatId(this CallbackQuery query)
        {
            return query.Message.CurrentChatId();
        }
    }
}
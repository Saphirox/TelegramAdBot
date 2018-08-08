using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramAdBot.Services
{
    public interface IReplyCommand
    {
        Task HandleReply(Message message);

        bool IsAppropriate(Message message);
    }
}
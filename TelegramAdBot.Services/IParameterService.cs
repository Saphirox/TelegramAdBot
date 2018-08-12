using System.Threading.Tasks;

namespace TelegramAdBot.Services
{
    public interface IParameterService
    {
        Task SendAsync(long chatId, string queryName, int priority = 1);
    }
}
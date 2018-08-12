namespace TelegramAdBot.Services
{
    public interface IParameterService
    {
        void SendAsync(long chatId, string queryId, int priority = 1);
    }
}
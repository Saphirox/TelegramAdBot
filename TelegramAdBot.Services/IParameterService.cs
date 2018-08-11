namespace TelegramAdBot.Services
{
    public interface IParameterService
    {
        void SendAsync(long chatId, string queryId);
    }
}
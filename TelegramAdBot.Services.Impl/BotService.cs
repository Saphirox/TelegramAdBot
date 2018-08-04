using Telegram.Bot;
 using TelegramAdBot.Configurations;
 
 namespace TelegramAdBot.Services.Impl
 {
     public class BotService : IBotService
     {
         public TelegramBotClient Client { get; set; }
     }
 }
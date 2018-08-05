using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramAdBot.Configurations;

namespace TelegramAdBot.Services.Impl
 {
     public class BotService : IBotService
     {
         public TelegramBotClient Client { get; set; }

         private readonly IOptions<BotConfiguration> _conf;

         public BotService(TelegramBotClient client, IOptions<BotConfiguration> conf)
         {
             Client = client;
             _conf = conf;
             SetWebhook();
         }

         public void SetWebhook()
         {
             Client.SetWebhookAsync(_conf.Value.Url);
         }
     }
 }
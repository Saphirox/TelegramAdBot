using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramAdBot.DataAccess;
using TelegramAdBot.Services.Impl.Helpers;

namespace TelegramAdBot.Services.Impl
{
    public class ParameterService : IParameterService
    {
        private readonly IChannelParameterRepository _cpr;
        private readonly ServiceHelper _serviceHelper;
        private readonly IBotService _bot;

        public ParameterService(IChannelParameterRepository cpr, ServiceHelper serviceHelper, IBotService bot)
        {
            _cpr = cpr;
            _serviceHelper = serviceHelper;
            _bot = bot;
        }

        public async void SendAsync(long chatId, string queryId)
        {
            const int firstPriority = 1;
                
            var parameter = await _serviceHelper.TryCatchAsync(async () => await _cpr.GetByPriorityAsync(firstPriority));

            
            const string callbackDataPattern = "{0} {1} {2}";
            
            var topics = parameter.Result.ChannelTopics.Select(c => new InlineKeyboardButton()
            {
                CallbackData = SerializeDto.SerializeFrom(
                    "Parameter", 
                    string.Format(callbackDataPattern, queryId, parameter, c.Id)).Value,
                Text = c.Name
            })
                .ToList();

            var linkedList = new List<InlineKeyboardButton[]>();

            var counter = 0;
            
            while (true)
            {
                if (topics.IndexOf(topics[counter])  == -1)
                    break;

                var s1 = topics[counter];
                counter++;
                
                var s2 = topics[counter];
                counter++;

                if (topics.IndexOf(topics[counter]) == -1)
                {
                    linkedList.Add(new [] { s1 });
                    break;
                }
                else
                {
                    linkedList.Add(new [] { s1, s2 });
                }
            }

            var nextPriority = firstPriority + 1;
            
            linkedList.Add(new [] {
                new InlineKeyboardButton 
                { 
                    Text = "Next parameter", 
                    CallbackData = CallbackDataBuilderExtentions.Serialize("NextParameter", nextPriority.ToString()) 
                }
            });
            
            var keyboard = new InlineKeyboardMarkup(linkedList);

            var forwardMessage = "Choose one of category";

            await _bot.Client.SendTextMessageAsync(chatId, forwardMessage, replyMarkup: keyboard);
        }
    }
}
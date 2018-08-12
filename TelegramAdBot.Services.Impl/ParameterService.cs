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

        public async void SendAsync(long chatId, string queryId, int priority)
        {
            var parameter = await _serviceHelper.TryCatchAsync(async () => await _cpr.GetByPriorityAsync(priority));

            const string callbackDataPattern = "{0} {1} {2}";
            
            var topics = parameter.Result.ChannelTopics.Select(c => new InlineKeyboardButton()
            {
                CallbackData = SerializeDto.SerializeFrom(
                    "Parameter", 
                    string.Format(callbackDataPattern, queryId, parameter.Result.Id, c.Name)).Value,
                Text = c.Name
            })
                .ToList();

            var linkedList = new List<InlineKeyboardButton[]>();

            var counter = 0;
            
            while (true)
            {
                if (counter >= topics.Count)
                    break;

                var s1 = topics[counter];
                counter++;
                
                if (counter >= topics.Count)
                {
                    linkedList.Add(new [] { s1 });
                    break;
                }
                
                var s2 = topics[counter];
                counter++;
                
                linkedList.Add(new [] { s1, s2 });
            }

            var minPriority = await _cpr.GetMinPriority();
            
            var nextPriority =  minPriority + 1;
            
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
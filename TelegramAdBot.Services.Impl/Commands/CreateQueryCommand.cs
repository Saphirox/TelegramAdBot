using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramAdBot.DataAccess;
using TelegramAdBot.Entities;
using TelegramAdBot.Services.Impl.Helpers;

namespace TelegramAdBot.Services.Impl.Commands
{
    public class CreateQueryCommand : IReplyCommand
    {
        private readonly IBotService _bot;
        private readonly IChannelParameterRepository _parameterRepository;
        private readonly IChannelQueryRepository _queryRepository;
        private readonly ServiceHelper _serviceHelper;

        public CreateQueryCommand(IBotService bot, IChannelParameterRepository parameterRepository, ServiceHelper serviceHelper, IChannelQueryRepository queryRepository)
        {
            _bot = bot;
            _parameterRepository = parameterRepository;
            _serviceHelper = serviceHelper;
            _queryRepository = queryRepository;
        }

        public async Task HandleReply(Message message)
        {
            var replyMessage = message.ReplyToMessage.Text;

            if (replyMessage == MessageConstants.EnterQuery)
            {
                var queryModel = new ChannelQuery(message.Text);

                await _serviceHelper
                    .TryCatchAsync(async () => await _queryRepository.AddAsync(queryModel));


                const int firstPriority = 1;
                
                var parameter = await _serviceHelper.TryCatchAsync(async () => await _parameterRepository.GetByPriorityAsync(firstPriority));

                var topics = parameter.Result.ChannelTopics.Select(c => new InlineKeyboardButton()
                {
                    CallbackData = CallbackDataBuilder.Serialize("Parameter", c.Name),
                    Text = c.Name
                }).ToList();


                var linkedList = new LinkedList<InlineKeyboardButton[]>(); 
                
                for (int i = 0; i < topics.Count / 2; i++)
                {
                    if (i == topics.Count-1)
                    {
                        linkedList.AddLast(new[] { topics[i*2] });
                    }
                    
                    linkedList.AddLast(new [] {topics[i*2], topics[i*2]});
                }
                
                var keyboard = new InlineKeyboardMarkup(linkedList);

                var forwardMessage = "Choose one of category";

                await _bot.Client.SendTextMessageAsync(message.CurrentChatId(), forwardMessage, replyMarkup: keyboard);
            }
            else
            {
                throw new ArgumentException(nameof(message));
            }
        }

        public bool IsAppropriate(Message message)
        {
            var replyMessage = message.ReplyToMessage.Text;

            return replyMessage == MessageConstants.EnterQuery;
        }
    }
}
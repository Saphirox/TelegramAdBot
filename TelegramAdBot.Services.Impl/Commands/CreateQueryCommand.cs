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
        private readonly IChannelQueryRepository _queryRepository;
        private readonly ServiceHelper _serviceHelper;
        private readonly IParameterService _parameterService;

        public CreateQueryCommand(ServiceHelper serviceHelper, IChannelQueryRepository queryRepository, IParameterService parameterService)
        {
            _serviceHelper = serviceHelper;
            _queryRepository = queryRepository;
            _parameterService = parameterService;
        }

        public async Task HandleReply(Message message)
        {
            var replyMessage = message.ReplyToMessage.Text;

            if (replyMessage == MessageConstants.EnterQuery)
            {
                var queryModel = new ChannelQuery(message.Text);

                var s = await _serviceHelper
                    .TryCatchAsync(async () => await _queryRepository.AddAsync(queryModel));

                _parameterService.SendAsync(message.CurrentChatId(), s.Result.Id);
            }
            else
            {
                throw new ArgumentException(nameof(message));
            }
        }

        public bool IsAppropriate(Message message)
        {
            var replyMessage = message?.ReplyToMessage.Text;

            return !string.IsNullOrEmpty(replyMessage) && replyMessage == MessageConstants.EnterQuery;
        }
    }
}
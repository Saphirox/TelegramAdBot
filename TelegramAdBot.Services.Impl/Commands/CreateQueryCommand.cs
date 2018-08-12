using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.DataAccess;
using TelegramAdBot.Entities;
using TelegramAdBot.Services.Impl.Helpers;

namespace TelegramAdBot.Services.Impl.Commands
{
    public class CreateQueryCommand : IReplyCommand
    {
        private readonly IChannelQueryRepository _queryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ServiceHelper _serviceHelper;
        private readonly IParameterService _parameterService;

        public CreateQueryCommand(ServiceHelper serviceHelper, IChannelQueryRepository queryRepository, IParameterService parameterService, IUserRepository userRepository)
        {
            _serviceHelper = serviceHelper;
            _queryRepository = queryRepository;
            _parameterService = parameterService;
            _userRepository = userRepository;
        }

        public Task HandleReply(Message message)
        {
            var replyMessage = message.ReplyToMessage.Text;

            if (replyMessage == MessageConstants.EnterQuery && message.Text != null)
            {
                var queryModel = new ChannelQuery(message.Text);

                var user = CurrentUser.Instance;

                if (user.Queries is null)
                {
                    user.Queries = new List<ChannelQuery>();
                }
                
                user.Queries.Add(queryModel);
                
                _serviceHelper
                    .TryCatch(() => _userRepository.UpdateAsync(user));

                _parameterService.SendAsync(message.CurrentChatId(), queryModel.Name);
            }
            else
            {
                throw new ArgumentException(nameof(message));
            }

            return Task.CompletedTask;
        }

        public bool IsAppropriate(Message message)
        {
            var replyMessage = message?.ReplyToMessage?.Text;

            var mess = message?.Text;

            return !string.IsNullOrEmpty(replyMessage) && !string.IsNullOrEmpty(mess) && replyMessage == MessageConstants.EnterQuery;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.DataAccess;
using TelegramAdBot.Entities;
using TelegramAdBot.Services.Handlers;
using TelegramAdBot.Services.Impl.Helpers;

namespace TelegramAdBot.Services.Impl.Handlers
{
    public class ChooseParameterHandler : ICallbackQuery
    {
        private readonly IBotService _bot;
        private readonly IUserRepository _userRepository;
        private readonly ServiceHelper _serviceHelper;
        private readonly IParameterService _parameterService;
        private readonly IChannelParameterRepository _cpr;
        
        public ChooseParameterHandler(IBotService bot, IUserRepository userRepository, ServiceHelper serviceHelper, IParameterService parameterService, IChannelParameterRepository cpr)
        {
            _bot = bot;
            _userRepository = userRepository;
            _serviceHelper = serviceHelper;
            _parameterService = parameterService;
            _cpr = cpr;
        }
        
        public bool IsAppropriate(CallbackQuery query)
        {
            return new SerializeDto(query).Deserialize().Key == CallbackConstants.Topic;
        }

        public async Task HandleCallbackAsync(CallbackQuery cquery)
        {
            var dto = new SerializeDto(cquery).Deserialize();

            var data = dto.Value.Split(" ");

            var queryName = data[0];
            var parameterId = data[1];

            var indexOf = dto.Value.IndexOf(parameterId) + parameterId.Length + 1;
            
            var topicName = dto.Value.Substring(indexOf, dto.Value.Length - indexOf);

            var queries = CurrentUser.Instance.Queries.Single(c => c.Name == queryName);

            if (queries.ChannelParameters == null)
            {
                var @params = queries.ChannelParameters = new List<ChannelParameter>();

                var entityParam = await _serviceHelper.TryCatchAsync(async () =>await _cpr.GetByIdAsync(parameterId));

                if (entityParam.Status == OptionResult.None)
                {
                    throw new ArgumentException(nameof(OptionResult));
                }

                var param = new ChannelParameter()
                {
                    Name = entityParam.Result.Name,
                    ChannelTopics = entityParam.Result.ChannelTopics.Where(c => c.Name == topicName).ToList()
                };
                
                @params.Add(param);
            }
            else
            {
                var @params = queries.ChannelParameters.Single(c => c.Id == parameterId);
            
                var topic = @params.ChannelTopics.FirstOrDefault(l => topicName == l.Name);

                if (topic != null)
                {
                    var newTopics = @params.ChannelTopics.Where(c => c.Name != topicName).ToList();

                    @params.ChannelTopics = newTopics;
                }
                else
                {
                    var topicEntity = (await _cpr.GetByIdAsync(parameterId)).ChannelTopics.SingleOrDefault(c => c.Name == topicName);
                
                    @params.ChannelTopics.Add(topicEntity);
                }
            }

            var result = _serviceHelper.TryCatch(() => _userRepository.UpdateAsync(CurrentUser.Instance)).Status;

            switch(result)
            {
                case OptionResult.None:
                    await _bot.Client.SendTextMessageAsync(cquery.Message.Chat.Id, "Error was hapend");                   
                    break;
                case OptionResult.Some:
                    await _parameterService.SendAsync(cquery.Message.CurrentChatId(), queryName);
                    break;
            }
        }
    }
}
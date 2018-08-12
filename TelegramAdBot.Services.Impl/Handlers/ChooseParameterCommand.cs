using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.DataAccess;
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
            return Model.CanSerialize(query);
        }

        public async Task HandleCallbackAsync(CallbackQuery query)
        {
            var dto = new SerializeDto(query).Deserialize();

            var data = dto.Value.Split(" ");

            var parameterId = data[1];
            var queryName = data[0];
            var topicName = data[2];

            var quer = CurrentUser.Instance.Queries.Single(c => c.Name == queryName);

            var @params = quer.ChannelParameters.Single(c => c.Id == parameterId);
            
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

            var result = _serviceHelper.TryCatch(() => _userRepository.UpdateAsync(CurrentUser.Instance)).Status;

            switch(result)
            {
                case OptionResult.None:
                    await _bot.Client.SendTextMessageAsync(query.Message.Chat.Id, "Error was hapend");                   
                    break;
                case OptionResult.Some:
                    await _parameterService.SendAsync(query.Message.CurrentChatId(), queryName);
                    break;
            }
        }
        
        private class Model
        {
            public string QueryId { get; set; }

            public string ParameterId { get; set; }
            
            public string TopicId { get; set; }

            public static bool CanSerialize(CallbackQuery query)
              =>  new SerializeDto(query).Deserialize().Value.Split(" ").Length == 3;
        }
    }
}
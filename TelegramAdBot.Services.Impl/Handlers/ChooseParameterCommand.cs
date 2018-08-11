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
        private readonly IChannelTopicRepository _ctr;
        private readonly IParameterService _parameterService;
        
        public ChooseParameterHandler(IBotService bot, IUserRepository userRepository, ServiceHelper serviceHelper, IChannelTopicRepository ctr, IParameterService parameterService)
        {
            _bot = bot;
            _userRepository = userRepository;
            _serviceHelper = serviceHelper;
            _ctr = ctr;
            _parameterService = parameterService;
        }
        
        public bool IsAppropriate(CallbackQuery query)
        {
            return Model.CanSerialize(query);
        }

        public async Task HandleCallbackAsync(CallbackQuery query)
        {
            var dto = new SerializeDto(query).Deserialize();

            var s = dto.Value.Split(" ");

            var paramaterId = s[1];
            var queryId = s[0];
            var topicId = s[2];

            var quer = CurrentUser.Instance.Queries.Single(c => c.Id == queryId);

            var @params = quer.ChannelParameters.Single(c => c.Id == paramaterId);
            
            var topic = @params.ChannelTopics.FirstOrDefault(l => topicId == l.Id);

            if (topic != null)
            {
                var newTopics = @params.ChannelTopics.Where(c => c.Id != topicId).ToList();

                @params.ChannelTopics = newTopics;
            }
            else
            {
                var topicEntity = await _ctr.GetByIdAsync(topicId);
                
                @params.ChannelTopics.Add(topicEntity);
            }

            var result = _serviceHelper.TryCatch(() => _userRepository.UpdateAsync(CurrentUser.Instance)).Status;

            switch(result)
            {
                case OptionResult.None:
                    await _bot.Client.SendTextMessageAsync(query.Message.Chat.Id, "Error was hapend");                   
                    break;
                case OptionResult.Some:
                    _parameterService.SendAsync(query.Message.CurrentChatId());
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
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.Services.Handlers;
using TelegramAdBot.Services.Impl.Helpers;

namespace TelegramAdBot.Services.Impl.Handlers
{
    public class NextParameterHandler : ICallbackQuery
    {
        private readonly IParameterService _parameterService;
        
        public NextParameterHandler(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }
        
        public bool IsAppropriate(CallbackQuery query)
        
        {
           var dto = new SerializeDto(query).Deserialize();

           return dto.Key == "NextParameter";
        }

        public async Task HandleCallbackAsync(CallbackQuery cquery)
        {
            var dto = new SerializeDto(cquery).Deserialize();

            var tokens = dto.Value.Split(" ");

            var queryName = tokens[0];
            var priority = int.Parse(tokens[1]);
            
            await _parameterService.SendAsync(cquery.CurrentChatId(), queryName, priority);
        }
    }
}
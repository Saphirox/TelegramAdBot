using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramAdBot.Services;

namespace TelegramAdBot.WebApi.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IBotService _bot;
        
        public MessageController(IMessageService messageService, IBotService bot)
        {
            this._messageService = messageService;
            _bot = bot;
        }
        
        [HttpPost]
        [Route(@"api/message/update")]
        public async Task<IActionResult> Update([FromBody]Update update)
        {
            await _messageService.ExecuteAsync(update);

            return Ok();
        }
    }
} 
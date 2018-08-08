using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramAdBot.Services;

namespace TelegramAdBot.WebApi.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        
        public MessageController(IMessageService messageService)
        {
            this._messageService = messageService;
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
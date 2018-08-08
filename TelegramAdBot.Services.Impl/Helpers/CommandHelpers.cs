using System;
using Microsoft.Extensions.Logging;

namespace TelegramAdBot.Services.Impl.Helpers
{
    public static class CommandHelpers
    {
        public static void TryCatch<T>(this ILogger logger, Func<T> action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }
        }        
    }
}
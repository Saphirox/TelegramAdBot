using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TelegramAdBot.Services.Impl.Helpers
{
    public class ServiceHelper
    {
        private readonly ILogger _logger;
        
        public ServiceHelper(ILogger logger)
        {
            this._logger = logger;
        }
        
        public Option<T> TryCatch<T>(Func<T> action)
        {
            var result = new Option<T>();
            
            try
            {
                result.Result = action();
                result.Status = OptionResult.Some;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                result.Status = OptionResult.None;
            }

            return result;
        }
        
        public async Task<Option<T>> TryCatchAsync<T>(Func<Task<T>> action)
        {
            var result = new Option<T>();
            
            try
            {
                result.Result = await action();
                result.Status = OptionResult.Some;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                result.Status = OptionResult.None;
            }

            return result;
        }
    }

    public static class ServiceHelperExtention
    {
        public static Option<T> TryCatch<T>(this Option<T> option, Func<T> action)
        {
            if (option.Status == OptionResult.None)
            {
                return option;
            }
            
            var result = new Option<T>();
            
            try
            {
                result.Result = action();
                result.Status = OptionResult.Some;
            }
            catch (Exception e)
            {
                result.Status = OptionResult.None;
            }

            return result;
        }
    }
}
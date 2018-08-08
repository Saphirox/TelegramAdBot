using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TelegramAdBot.Services.Impl.Helpers
{
    public static class CallbackDataBuilder
    {
        private const string Template = "{0},{1}";
        
        public static string Serialize(string callbackName, string data)
        {
            return string.Format(Template, callbackName, data);
        }

        public static KeyValuePair<string, string> Deserialize(string serializedData)
        {
            var value = serializedData.Split(",");

            if (value.Length != 2)
            {
                throw new ArgumentException(nameof(serializedData));
            }

            return new KeyValuePair<string, string>(value[0], value[1]);
        }
    }
}
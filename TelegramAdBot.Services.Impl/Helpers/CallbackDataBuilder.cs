using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace TelegramAdBot.Services.Impl.Helpers
{
    public static class CallbackDataBuilderExtentions
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

        public static KeyValuePair<string, string> Deserialize(this SerializeDto dto)
        {
            return Deserialize(dto.Value);
        }
    }

    public struct SerializeDto
    {
        private const string Template = "{0},{1}";
        
        public SerializeDto(string value)
        {
            Value = value;
        }
        
        public SerializeDto(CallbackQuery query)
        {
            Value = query.Data;
        }
        
        public static SerializeDto SerializeFrom(string callbackName, string data)
        {
            return new SerializeDto(string.Format(Template, callbackName, data));
        }
        
        public string Value { get; set; }
    }
}
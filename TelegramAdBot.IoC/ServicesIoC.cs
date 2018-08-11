using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramAdBot.Configurations;
using TelegramAdBot.Services;
using TelegramAdBot.Services.Handlers;
using TelegramAdBot.Services.Impl;
using TelegramAdBot.Services.Impl.Commands;
using TelegramAdBot.Services.Impl.Handlers;
using TelegramAdBot.Services.Impl.Helpers;

namespace TelegramAdBot.IoC
{
    public static class ServicesIoC
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton(
                (r) => new TelegramBotClient(r.GetService<IOptions<BotConfiguration>>().Value.Token));
            
            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<IBotCommandsFactory, BotCommandsFactory>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IParameterService, ParameterService>();

            services.AddTransient<ICommand, ChooseRoleCommand>();
            services.AddTransient<ICommand, CreateQueryReply>();
            
            services.AddTransient<ICallbackQuery, ChooseRoleHandler>();
            services.AddTransient<ICallbackQuery, ChooseParameterHandler>();

            services.AddTransient<IReplyCommand, CreateQueryCommand>();

            services.AddTransient((r) => new Lazy<IEnumerable<ICommand>>(r.GetServices<ICommand>()));
            services.AddTransient((r) => new Lazy<IEnumerable<ICallbackQuery>>(r.GetServices<ICallbackQuery>()));
            services.AddTransient((r) => new Lazy<IEnumerable<IReplyCommand>>(r.GetServices<IReplyCommand>()));
            
            // Tools
            services.AddTransient<ServiceHelper>();
            
            return services;
        }
    }
}

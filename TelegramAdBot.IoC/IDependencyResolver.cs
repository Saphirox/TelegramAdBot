using System;
namespace TelegramAdBot.IoC
{
    public interface IDependencyResolver
    {
        TService GetService<TService>();
    }
}

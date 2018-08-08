namespace TelegramAdBot.Services.Impl.Helpers
{

    public class Option
    {
        public OptionResult Status { get; set; }
    }
    
    public class Option<T> : Option
    {
        public T Result { get; set; }
    }
}
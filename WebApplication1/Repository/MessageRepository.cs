using Microsoft.Extensions.Options;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    //public class MessageRepository(IOptions<NewBookAlertConfig> newBookAlertConfiguration) : IMessageRepository
    //public class MessageRepository(IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration) : IMessageRepository
    public class MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfiguration) : IMessageRepository
    {
        //private readonly NewBookAlertConfig _newBookAlertConfiguration = newBookAlertConfiguration.Value;
        //private readonly NewBookAlertConfig _newBookAlertConfiguration = newBookAlertConfiguration.CurrentValue;
        private readonly IOptionsMonitor<NewBookAlertConfig> _newBookAlertConfiguration = newBookAlertConfiguration;
        //_newBookAlertConfiguration.OnChange(config=>_newBookAlertConfiguartion=config);


        public string GetName()
        {
            //return _newBookAlertConfiguration.BookName;
            return _newBookAlertConfiguration.CurrentValue.BookName;

        }
    }
}

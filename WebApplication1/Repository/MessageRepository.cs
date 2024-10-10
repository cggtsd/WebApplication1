using Microsoft.Extensions.Options;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    //public class MessageRepository(IOptions<NewBookAlertConfig> newBookAlertConfiguration) : IMessageRepository
    //public class MessageRepository(IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration) : IMessageRepository
    public class MessageRepository: IMessageRepository
    {
        //private readonly NewBookAlertConfig _newBookAlertConfiguration = newBookAlertConfiguration.Value;
        //private NewBookAlertConfig _newBookAlertConfiguration;

        private readonly IOptionsMonitor<NewBookAlertConfig> _newBookAlertConfiguration ;
        
        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfiguration)
        {
            _newBookAlertConfiguration = newBookAlertConfiguration;
            //newBookAlertConfiguration.OnChange(config=> _newBookAlertConfiguration = config);
        }


        public string GetName()
        {
            return _newBookAlertConfiguration.CurrentValue.BookName;
            //return _newBookAlertConfiguration.CurrentValue.BookName;

        }
    }
}

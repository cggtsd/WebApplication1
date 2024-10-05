using Microsoft.Extensions.Options;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class MessageRepository: IMessageRepository
    {
        private readonly NewBookAlertConfig _newBookAlertConfiguration;
        public MessageRepository(IOptions<NewBookAlertConfig> newBookAlertConfiguration)
        {
            _newBookAlertConfiguration = newBookAlertConfiguration.Value;
        }
        public string GetName()
        {
            return _newBookAlertConfiguration.BookNamw;
        }
    }
}

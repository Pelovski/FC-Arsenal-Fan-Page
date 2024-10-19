using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCArsenalFanPage.Services.Messaging
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            throw new System.NotImplementedException();
        }
    }
}

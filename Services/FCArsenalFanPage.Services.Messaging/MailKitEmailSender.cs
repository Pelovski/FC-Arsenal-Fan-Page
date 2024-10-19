namespace FCArsenalFanPage.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;

    public class MailKitEmailSender : IEmailSender
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string smtpUser;
        private readonly string smtpPass;

        public MailKitEmailSender(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.smtpUser = smtpUser;
            this.smtpPass = smtpPass;
        }

        public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName ?? "YourAppName", from ?? this.smtpUser));
            message.To.Add(new MailboxAddress(string.Empty, to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlContent };

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    var contentType = MimeKit.ContentType.Parse(attachment.MimeType);
                    bodyBuilder.Attachments.Add(attachment.FileName, attachment.Content, contentType);
                }
            }

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(this.smtpServer, this.smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(this.smtpUser, this.smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}

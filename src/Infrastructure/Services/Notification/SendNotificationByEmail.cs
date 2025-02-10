using Application.Interface;
using Infrastructure.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services.Notification
{
    public class SendNotificationByEmail(IOptions<MailSetting> mailSetting) : ISendNotification
    {
        private readonly MailSetting _mailSetting = mailSetting.Value 
                                                    ?? throw new ArgumentNullException(nameof(MailSetting));
        
        public async Task SendAsync(string to, string body, string subject, string? nameTo = null, bool isLink = false)
        {
            var emailMessage = new MimeMessage();
            var emailForm = new MailboxAddress(_mailSetting.Name, _mailSetting.EmailId);
            emailMessage.From.Add(emailForm);
            nameTo ??= to.Split("@")[0];
            var emailTo = new MailboxAddress(nameTo, to);
            emailMessage.To.Add(emailTo);
            emailMessage.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            if (!isLink)
            {
                bodyBuilder.TextBody = body;
            }
            else
            {
                bodyBuilder.HtmlBody = body;
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            var mailClient = new SmtpClient();
            await mailClient.ConnectAsync(_mailSetting.Host, _mailSetting.Port, _mailSetting.UseSsl);
            await mailClient.AuthenticateAsync(_mailSetting.EmailId, _mailSetting.Password);
            await mailClient.SendAsync(emailMessage);
            await mailClient.DisconnectAsync(true);
            mailClient.Dispose();
        }
    }
}
using Application.Interface;
using Infrastructure.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services.Notification
{
    public class SendNotificationByEmail(MailSetting mailSetting) : ISendNotification
    {
        public async Task SendAsync(string to, string body, string subject, string? nameTo = null, bool isLink = false)
        {
            var emailMessage = new MimeMessage();
            var emailForm = new MailboxAddress(mailSetting.Name, mailSetting.EmailId);
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
            await mailClient.ConnectAsync(mailSetting.Host, mailSetting.Port, mailSetting.UseSsl);
            await mailClient.AuthenticateAsync(mailSetting.EmailId, mailSetting.Password);
            await mailClient.SendAsync(emailMessage);
            await mailClient.DisconnectAsync(true);
            mailClient.Dispose();
        }
    }
}
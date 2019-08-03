using SWE.JOIN.CrossCutting.SmtpEmailSender;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SWE.JOIN.CrossCutting.Framework.SmtpEmailSender
{
    public class EmailSender : ISmtpEmailSender
    {
        public void Send(EmailProperties email)
        {
            var mail = new MailMessage
            {
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                From = new MailAddress(email.From, email.DisplayName)
            };

            foreach (var to in email.To.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                mail.To.Add(to);
            }

            foreach (var bcc in email.Bcc)
            {
                mail.Bcc.Add(bcc);
            }

            foreach (var attachment in email.Attachments)
            {
                mail.Attachments.Add(new Attachment(new MemoryStream(attachment.Item2), attachment.Item1));
            }

            var smtpClient = new SmtpClient
            {
                Host = email.Host,
                Port = email.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email.From, email.Password),
                EnableSsl = true
            };

            Task.Run(() => smtpClient.Send(mail));
        }
    }
}
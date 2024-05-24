using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace xilopro2.Helpers.Interfaces
{
    public interface IMailService
    {
        Task SendEmailServiceAsync(string email, string subject, string message);
    }

    public class EmailSenderService : IMailService
    {
        public readonly string mymail = "eabucam@outlook.com";
        public readonly string pass = "Metformina23+";

        public async Task SendEmailServiceAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mymail,pass)
            };
            MailMessage mailMessage = new MailMessage(mymail, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true, // Set to true if your message is in HTML format
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
            };
           // return client.SendMailAsync(mailMessage);
            try
            {
                client.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
           // return ;
            /*    return client.SendMailAsync(
                    new MailMessage(from: mymail,
                                    to: email,
                                    subject,
                                    message
                                    ));*/
        }
    }



}

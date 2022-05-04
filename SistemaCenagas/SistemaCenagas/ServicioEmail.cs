using MailKit.Net.Smtp;
using MimeKit;
using SistemaCenagas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCenagas
{
    public static class ServicioEmail
    {

        public static string EMAIL_ADDRESS { get; set; }
        public static string EMAIL_PASSWORD { get; set; }
        public static string EMAIL_SERVER { get; set; }

        public static string SendEmailResetPassword(Usuarios user, string action, string subject, string bodyText)
        {
            string url = $"{EMAIL_SERVER}/Home/{action}?email={user.Email}";
            string emailText = bodyText + $"<a href='{url}'>Clic aquí</a>";
            string fromAddress = EMAIL_ADDRESS;
            string password = EMAIL_PASSWORD;
            string toAddress = user.Email;


            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
                    client.Authenticate(fromAddress, password);

                    var body = new BodyBuilder
                    {
                        HtmlBody = emailText//$"<p>Body test</p>",
                        //TextBody = emailText
                    };
                    var message = new MimeMessage
                    {
                        Body = body.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Administración de CENAGAS", fromAddress));
                    message.To.Add(new MailboxAddress("", toAddress));
                    message.Subject = subject;
                    client.Send(message);
                    client.Disconnect(true);
                }
                return "Envio exitoso! :)";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public static string SendEmailNotification(Usuarios user, string subject, string bodyText)
        {
            string fromAddress = EMAIL_ADDRESS;
            string password = EMAIL_PASSWORD;
            string toAddress = user.Email;

            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
                    client.Authenticate(fromAddress, password);

                    var body = new BodyBuilder
                    {
                        HtmlBody = bodyText//$"<p>Body test</p>",
                        //TextBody = emailText
                    };
                    var message = new MimeMessage
                    {
                        Body = body.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Administración de CENAGAS", fromAddress));
                    message.To.Add(new MailboxAddress("", toAddress));
                    message.Subject = subject;
                    client.Send(message);
                    client.Disconnect(true);
                }
                return "Envio exitoso! :)";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }


    }
}

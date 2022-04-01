using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Ded_Project
{
    static class Email
    {
        static public bool Send(string subject, string body, string email)
        {
            int port = 587;
            string server = "smtp.gmail.com";
            string serverName = "testing.servvv@gmail.com";
            string serverPassword = "1L9d6Hq9!";
            MailAddress from = new MailAddress(serverName);   
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(server, port);
            smtp.Credentials = new NetworkCredential(serverName, serverPassword);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(message);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

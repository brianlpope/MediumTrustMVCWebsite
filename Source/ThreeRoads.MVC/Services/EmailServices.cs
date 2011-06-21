using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace ThreeRoads.MVC.Services
{
    public class EmailServices
    {
        public void SendEmailMessage(string fromName, string fromAddress, string body, string subject, string toAddress, string toName)
        {
            var emailmessage = new MailMessage();
            emailmessage.Subject = subject;
            emailmessage.From = new MailAddress(fromAddress, fromName);
            emailmessage.To.Add(new MailAddress(toAddress, toName));
            emailmessage.Body = body;

            var smtpClient = new SmtpClient();
            smtpClient.Send(emailmessage);
        }
    }
}
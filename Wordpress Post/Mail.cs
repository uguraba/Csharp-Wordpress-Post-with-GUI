using System;
using System.Net.Mail;
using System.Net;

namespace Wordpress_Post
{
    class Mail
    {
        public static void sendMail(string _mailTo, string _mailBody)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("ugur.aba@gmail.com", "BQJ8QTRJ7QRRW9VVHP9H");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("eposta@gmail.com", "C# Wordpress Post");
            mail.To.Add(_mailTo);
            mail.Subject = "C# Wordpress Post";
            mail.IsBodyHtml = true;
            mail.Body = _mailBody;
            //mail.Attachments.Add(new Attachment(@"C:\Rapor.xlsx"));
            sc.Send(mail);
        }
    }
}
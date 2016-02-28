using System.Net.Mail;
using System.Net;

/*
    DateTime: 27.02.2016 22:45 GMT+2
    Github: https://github.com/uguraba
    Twitter: https://twitter.com/uguraba
*/

namespace Wordpress_Post
{
    class Mail
    {
        public static void sendMail(string _mailTo, string _mailBody)
        {
            MailMessage _mail = new MailMessage();
            _mail.From = new MailAddress("ugur.aba@gmail.com", "C# Wordpress Post");
            _mail.To.Add(_mailTo);
            _mail.Subject = "C# Wordpress Post";
            _mail.IsBodyHtml = true;
            _mail.Body = _mailBody;
            //mail.Attachments.Add(new Attachment(@"C:\Rapor.xlsx"));
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.Port = 587;
            _smtpClient.Host = "smtp.gmail.com";
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential("ugur.aba@gmail.com", "BQJ8QTRJ7QRRW9VVHP9H");
            _smtpClient.Send(_mail);
        }
    }
}
#region Define Namespaces
using System.Net.Mail;
using System.Net;
#endregion

#region Information about this class
/*
    DateTime: 27.02.2016 22:45 GMT+2
    Github: https://github.com/uguraba
    Twitter: https://twitter.com/uguraba
*/
#endregion

namespace Wordpress_Post
{
    class Mail
    {
        #region sendMail Function
        public static void sendMail(string _sender, string _senderPassword, string _subject, string _mailTo, string _mailBody)
        {
            MailMessage _mail = new MailMessage();
            _mail.From = new MailAddress(_sender, _subject);
            _mail.To.Add(_mailTo);
            _mail.Subject = _subject;
            _mail.IsBodyHtml = true;
            _mail.Body = _mailBody;
            //mail.Attachments.Add(new Attachment(@"C:\Rapor.xlsx"));
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.Port = 587;
            _smtpClient.Host = "smtp.gmail.com";
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(_sender, _senderPassword);
            _smtpClient.Send(_mail);
        }
        #endregion
    }
}
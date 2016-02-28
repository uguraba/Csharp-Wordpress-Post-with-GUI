#region Define Namespaces
using System.Net;
using System.Net.Mail;
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
        #region Functions
        // sendMail member function will use for sending mail with using smtp
        // sendMail member function is also overloaded.
        
        /*
            In the first method it receive five strings.
            Strings represents sender mail, sender password, mail subject, target mail, mail body respectively.
            The method will return a void datatype.
        */
        public static void sendMail(string _sender, string _senderPassword, string _subject, string _mailTo, string _mailBody)
        {
            MailMessage _mail = new MailMessage();
            _mail.From = new MailAddress(_sender, _subject);
            _mail.To.Add(_mailTo);
            _mail.Subject = _subject;
            _mail.IsBodyHtml = true;
            _mail.Body = _mailBody;
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.Port = 587;
            _smtpClient.Host = "smtp.gmail.com"; // If sender host is hotmail, outlook etc. use "smtp.live.com";
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(_sender, _senderPassword);
            _smtpClient.SendMailAsync(_mail);
        }

        /*
            In the second method it receive five strings and one string array.
            Strings represents sender mail, sender password, mail subject, target mail, mail body respectively.
            String array represents attachment paths.
            The method will return a void datatype.
        */
        public static void sendMail(string _sender, string _senderPassword, string _subject, string _mailTo, string _mailBody, string[] _attachmentPaths)
        {
            MailMessage _mail = new MailMessage();
            _mail.From = new MailAddress(_sender, _subject);
            _mail.To.Add(_mailTo);
            _mail.Subject = _subject;
            _mail.IsBodyHtml = true;
            _mail.Body = _mailBody;
            if (_attachmentPaths.Length != 0)
            {
                for (int i = 0; i < _attachmentPaths.Length; i++)
                    _mail.Attachments.Add(new Attachment(_attachmentPaths[i]));
            }
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.Port = 587;
            _smtpClient.Host = "smtp.gmail.com"; // If sender host is hotmail, outlook etc. use "smtp.live.com";
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(_sender, _senderPassword);
            _smtpClient.SendMailAsync(_mail);
        }
        #endregion
    }
}
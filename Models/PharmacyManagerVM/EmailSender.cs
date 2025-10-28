using Microsoft.Extensions.Options;
using IbhayiPharmacy.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public void SendEmail(string to, string subject, string body, string from = null)
    {
        if (string.IsNullOrWhiteSpace(to))
            throw new ArgumentException("Recipient email address cannot be null or empty.", nameof(to));

        if (string.IsNullOrWhiteSpace(subject))
            subject = "(No Subject)";

        if (string.IsNullOrWhiteSpace(body))
            body = "(Empty message)";

        string senderEmail = from ?? _smtpSettings.Username ?? "mbasamajila001@gmail.com";

        using var smtpClient = new SmtpClient(_smtpSettings.Server ?? "smtp.gmail.com", _smtpSettings.Port == 0 ? 587 : _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(
                _smtpSettings.Username ?? "mbasamajila001@gmail.com",
                _smtpSettings.Password ?? "rwfv yxkc uits lgel"
            ),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);

        smtpClient.Send(mailMessage);
    }
}








//var mail = "mbasamajila001@gmail.com";
//var pw = "udyt gpxe olkx mjxd";

using Microsoft.Extensions.Options;
using IbhayiPharmacy.Models.PharmacyManagerVM;
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

    public void SendEmailAsync(string to, string subject, string body, string from = null)
    {
        if (string.IsNullOrWhiteSpace(to))
            throw new ArgumentException("Recipient email address cannot be null or empty.", nameof(to));

        if (string.IsNullOrWhiteSpace(subject))
            subject = "(No Subject)";

        if (string.IsNullOrWhiteSpace(body))
            body = "(Empty message)";

        var senderEmail = from ?? _smtpSettings.SenderEmail ?? "ibhayipharmacy0414@gmail.com";

        using var smtpClient = new SmtpClient(_smtpSettings.Host ?? "smtp.gmail.com", _smtpSettings.Port == 0 ? 587 : _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.SenderEmail ?? "ibhayipharmacy0414@gmail.com",
                                                _smtpSettings.Password ?? "wkfy phvj znru clyr"),
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

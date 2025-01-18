using System.Net.Mail;

namespace EmailSender;

public class Email
{
    public async Task SendEmail(Dictionary<string, string> properties)
    {
        var studentEmail = properties["email"];
        var smtpClient = new SmtpClient("smtp.example.com")
        {
            Port = 587,
            Credentials = new System.Net.NetworkCredential("email@email.com", "password"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("email@email.com"),
            Subject = "Welcome!",
            Body = "Test",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(studentEmail);

        smtpClient.Send(mailMessage);
    }
}
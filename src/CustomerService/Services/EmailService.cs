using CustomerService.Interfaces.External;
using System.Net.Mail;

namespace CustomerService.Services;

public class EmailService : IEmailService
{
    public async Task SendEmail(Dictionary<string, string> properties)
    {
        //TODO
    }
}

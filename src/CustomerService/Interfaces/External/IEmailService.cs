namespace CustomerService.Interfaces.External;

public interface IEmailService
{
    public Task SendEmail(Dictionary<string, string> properties);
}

namespace CustomerService.Models;

public class Customer
{
    public Customer(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        SetPassword(password);
    }

    public Guid Id { get; private set; }
    private string _name;
    private string _email;
    private string _password;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty or null.");
            _name = value;
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            //if (!IsValidEmail(value)) throw new ArgumentException("Invalid email format.");
            _email = value;
        }
    }

    public string Password
    {
        get => "********";
        private set
        {
            //if (!IsValidPassword(value)) throw new ArgumentException("Password must be at least 8 characters, include an uppercase letter and a number.");
            _password = value;
        }
    }

    public void UpdatePassword(string currentPassword, string newPassword)
    {
        if (_password != currentPassword) throw new UnauthorizedAccessException("Password is incorrect.");
        Password = newPassword;
    }

    public void SetPassword(string password)
    {
        //if (!IsValidPassword(password)) throw new ArgumentException("Password must be at least 8 characters, include an uppercase letter and a number.");
        _password = password;
    }

    public override string ToString() => $"Customer [Id: {Id}, Name: {Name}, Email: {Email}]";
    private bool IsValidEmail(string email) => email.Contains("@") && email.Contains(".");

    private static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8) return false;

        bool hasUpper = password.Any(char.IsUpper);
        bool hasLower = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);

        return hasUpper && hasLower && hasDigit;
    }
}

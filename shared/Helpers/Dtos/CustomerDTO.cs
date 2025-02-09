using DeliveryService.Models;

namespace Helpers.Dtos;

public class CustomerDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public CustomerDTO() { }

    public CustomerDTO(Guid id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    public static CustomerDTO FromEntity(Customer customer)
    {
        return new CustomerDTO
        {
            Name = customer.Name,
            Email = customer.Email
        };
    }
}
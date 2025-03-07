using Flowspire.Domain.Enums;

namespace Flowspire.API.Models;

public class RegisterRequest
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}

using Flowspire.Domain.Enums;

namespace Flowspire.Application.DTOs;
public class UserDTO
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public List<UserRole> Roles { get; set; }
}